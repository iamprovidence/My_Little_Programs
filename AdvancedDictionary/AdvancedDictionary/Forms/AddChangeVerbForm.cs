using System.Linq;
using System.Windows.Forms;
using AdvancedDictionary.Model;
using AdvancedDictionary.View;
using AdvancedDictionary.AdditionalClasses;

namespace AdvancedDictionary.Forms
{
    internal partial class AddChangeVerbForm : Form
    {
        // FIELDS
        Words verbs;
        Word data;
        // PROPERTIES
        public Word Data => data;
        // CONSTRUCTORS
        public AddChangeVerbForm(Words verbs)
        {
            InitializeComponent();
            this.verbs = verbs;
        }
        // METHODS
        public void Build(EmotionsRepository repos)// EMPTY FORM, ADD
        {
            this.Text = "Create";
            data = new Word();

            textTb.Clear();
            descriptionTb.Clear();

            emotionPm.Build( new EmotionsView( new Emotions(repos) ) );
            synonymsPm.Build( new SynonymsView( new Synonyms(verbs.Select(x => x.Text)) ) );
        }
        public void Build(Word data, EmotionsRepository repos)// CHANGE
        {
            this.Text = "Change";
            this.data = data;

            textTb.Text = data.Text;
            descriptionTb.Text = data.Description;

            // Repository could be changed, edited...            
            // ... change emotions list
            Algorithm.FullSet<string> setPickedAndUnpickedEmotions = Algorithm.DivideSetBySet(repos, data.Emotions.Picked);
            emotionPm.Build(new EmotionsView(
                new Emotions(Unpicked: setPickedAndUnpickedEmotions.Except, 
                             Picked: setPickedAndUnpickedEmotions.Intersect)));

            // if new words were added or edited...
            // ... change words list
            string[] verbArr = verbs.Select(x => x.Text).Where(x => x != data.Text).ToArray();// words list without choosen word
            Algorithm.FullSet<string> setPickedAndUnpickedSynonym = Algorithm.DivideSetBySet(verbArr, data.Synonyms.Picked);
            synonymsPm.Build(new SynonymsView(
                new Synonyms(Unpicked: setPickedAndUnpickedSynonym.Except,
                             Picked: setPickedAndUnpickedSynonym.Intersect)));
        }
        private void UpdateData()
        {
            string text = textTb.Text.Trim();
            if (text == string.Empty)
            {
                throw new System.Exception("Text field is empty");
            }
            if (data.Text != text && verbs.Contains(text))
            {
                throw new System.Exception("This word already in the list");
            }

            data.Text = text;
            data.Description = descriptionTb.Text.Trim();
            data.Emotions = new Emotions(emotionPm.Value.Unpicked, emotionPm.Value.Picked);
            data.Synonyms = new Synonyms(synonymsPm.Value.Unpicked, synonymsPm.Value.Picked);

            
        }
                
        // EVENTS
            // CLICK
        private void cancelBtn_Click(object sender, System.EventArgs e)
        {
            data = null;
            DialogResult = DialogResult.Cancel;
        }

        private void okBtn_Click(object sender, System.EventArgs e)
        {
            try
            {
                UpdateData();
                DialogResult = DialogResult.OK;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
