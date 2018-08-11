using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace FancyControls
{
    [DefaultEvent("Click")]
    [DefaultProperty("Value")]
    public partial class Counter : UserControl
    {
        // FIELDS
        int value;
        int minValue;
        int maxValue;
        int step;

        string text;
        string separator;

        bool isColorChangedPerTick;
        bool isMaxValueShowed;

        float valueFontSize;
        float textFontSize;

        ColorsPalette palette;
        Color[] colorsPalette;
        Color[] customColors;
        Color stdBackColor;
        Color textColor;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of the FancyControls.Counter class.
        /// </summary>
        public Counter()
        {
            InitializeComponent();

            ValueSize = countLbl.Font.Size;
            TextSize = textLbl.Font.Size;

            Minimum = 0;
            Maximum = 10;
            Value = 0;
            Step = 1;

            Text = "Incomplete Tasks";
            Separator = "/";

            ChangeColorPerTick = true;
            ShowMaxValue = true;

            palette = ColorsPalette.None;
            colorsPalette = null;
            customColors = null;
            stdBackColor = this.BackColor;
            textColor = Color.White;
        }

        // PROPERTIES
        /// <summary>
        /// Gets or sets the current value of the counter.
        /// </summary>
        /// <returns>
        /// The current value within the range  of the counter. The default id 0. 
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// The value specified is greater than the value of the FancyControls.Counter.Maximum property.
        /// -or- 
        /// The value specified is less than the value of the FancyControls.Counter.Minimum property.
        /// </exception>
        [Category("Behavior")]
        [Description("Gets or sets the current value of the counter.")]
        [DefaultValue(0)]
        public int Value
        {
            set
            {
                if (!IsValueInRange(value))
                {
                    throw new ArgumentException(
                        message:
                            $"Value of '{value}' is not valid for '{nameof(this.Value)}'. '{nameof(this.Value)}' should be between '{nameof(this.Minimum)}' and '{nameof(this.Maximum)}'.",
                        paramName: nameof(this.Value));
                }

                OnValueChanged(new ValueEventArgs<int>(oldValue: this.value, newValue: this.value = value));
            }
            get
            {
                return value;
            }
        }
        /// <summary>
        /// Gets or sets the minimum value of the range of the control.
        /// </summary>
        /// <returns>
        /// The minimum value of the range. The default is 0.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// The value specified for the property is greater than the value of the FancyControls.Counter.Maximum property.
        /// </exception>
        [Category("Behavior")]
        [Description("Gets or sets the minimum value of the range of the control.")]
        [DefaultValue(0)]
        public int Minimum
        {
            get
            {
                return minValue;
            }
            set
            {
                if(value > Maximum)
                {
                    throw new ArgumentException(
                       message:
                           $"Value of '{value}' is not valid for '{nameof(this.Minimum)}'. '{nameof(this.Minimum)}' should be less than '{nameof(this.Maximum)}'.",
                       paramName: nameof(this.Minimum));
                }
                OnMinimumChanged(new ValueEventArgs<int>(oldValue: minValue, newValue: minValue = value));
                
            }
        }
        /// <summary>
        /// Gets or sets the maximum value of the range of the control.
        /// </summary>
        /// <returns>
        /// The maximum value of the range. The default is 10.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// The value specified for the property is less than the value of the FancyControls.Counter.Minimum property.
        /// </exception>
        [Category("Behavior")]
        [Description("Gets or sets the maximum value of the range of the control.")]
        [DefaultValue(10)]
        public int Maximum
        {
            get
            {
                return maxValue;
            }
            set
            {
                if (value < Minimum)
                {
                    throw new ArgumentException(
                       message:
                           $"Value of '{value}' is not valid for '{nameof(this.Maximum)}'. '{nameof(this.Maximum)}' should be greater than '{nameof(this.Minimum)}'.",
                       paramName: nameof(this.Maximum));
                }
                OnMaximumChanged(new ValueEventArgs<int>(oldValue: maxValue, newValue: maxValue = value));
            }
        }
        /// <summary>
        /// Gets or sets the value will control change color per tick.
        /// </summary>
        /// <returns>
        /// The value specified will control change color per click.
        /// The default is true.
        /// </returns>
        [Category("Behavior")]
        [Description("Gets or sets the value will control change color per tick.")]
        [DefaultValue(true)]
        public bool ChangeColorPerTick
        {
            set
            {
                isColorChangedPerTick = value;
            }
            get
            {
                return isColorChangedPerTick;
            }
        }
        /// <summary>
        /// Gets or sets the value will control show the maximum value.
        /// </summary>
        /// <returns>
        /// The value specified will control show the maximum value.
        /// The default is true.
        /// </returns>
        [Category("Appearance")]
        [Description("Gets or sets the value will control show the maximum value.")]
        [DefaultValue(true)]
        public bool ShowMaxValue
        {
            get
            {
                return isMaxValueShowed;
            }
            set
            {
                isMaxValueShowed = value;
                OnShowMaxValueChanged(EventArgs.Empty);
            }
        }
        /// <summary>
        /// Gets or sets the amount by which a call to the FancyControls.Counter.Tick() method increases the current value of the counter.
        /// </summary>
        /// <returns>
        /// The amount by which to increment the counter with each call to the FancyControls.Counter.Tick() method. 
        /// The default is 1.
        /// </returns>
        [Category("Behavior")]
        [Description("The amount by which to increment the counter with each call to the FancyControls.Counter.Tick() method.")]
        [DefaultValue(1)]
        public int Step
        {
            set
            {
                OnStepChanged(new ValueEventArgs<int>(oldValue: step, newValue: step = value));
            }
            get
            {
                return step;
            }
        }
        /// <summary>
        /// Overrides System.Windows.Forms.Control.Text.
        /// </summary>
        /// <returns>
        /// The text associated with this control.
        /// </returns>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue("Incomplete Tasks")]
        public override string Text
        {
            set
            {
                OnTextChanged(new ValueEventArgs<string>(oldValue: this.text, newValue: text = value));
            }
            get
            {
                return text;
            }
        }
        /// <summary>
        /// Gets or sets the separator between value and the Maximum.
        /// </summary>
        /// <returns>
        /// The separator between value and the Maximum.
        /// The default is "/".
        /// </returns>
        [Category("Appearance")]
        [Description("Gets or sets the separator between value and the Maximum.")]
        [DefaultValue("/")]
        public string Separator
        {
            get
            {
                return separator;
            }
            set
            {
                OnSeparatorChanged(new ValueEventArgs<string>(oldValue: separator, newValue: separator = value));
            }
        }
        /// <summary>
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        /// <returns>
        /// The System.Drawing.Font to apply to the text displayed by the control. 
        /// The default is the value of the System.Windows.Forms.Control.DefaultFont property.
        /// </returns>
        [ReadOnly(true)]
        public override Font Font
        {
            get
            {
                return base.Font;
            }

            set
            {
                base.Font = value;
            }
        }
        /// <summary>
        /// Gets or sets text size.
        /// </summary>
        /// <returns>
        /// Text size.
        /// </returns>
        [Category("Appearance")]
        [Description("Gets or sets text size.")]
        [DefaultValue(60)]
        public float TextSize
        {
            get
            {
                return textFontSize;
            }
            set
            {
                OnTextSizeChanged(new ValueEventArgs<float>(oldValue: textFontSize, newValue: textFontSize = value));
            }
        }
        /// <summary>
        /// Gets or sets value size.
        /// </summary>
        /// <returns>
        /// Value size.
        /// </returns>
        [Category("Appearance")]
        [Description("Gets or sets value size.")]
        [DefaultValue(11)]
        public float ValueSize
        {
            get
            {
                return valueFontSize;
            }
            set
            {
                OnValueSizeChanged(new ValueEventArgs<float>(oldValue: valueFontSize, newValue: valueFontSize = value));
            }
        }

        /// <summary>
        /// Gets or sets the custom colors for the FancyControls.Counter control. 
        /// </summary>
        /// <returns>
        /// A FancyControls.CustomColors array of colors that determines the colors to be used.
        /// </returns> 
        [Category("Appearance")]
        [Description("Gets or sets the custom colors for the FancyControls.Counter control.")]
        [DefaultValue(null)]
        public Color[] CustomColors
        {
            get
            {
                return customColors;
            }
            set
            {
                palette = ColorsPalette.None;
                OnPaletteChanged(EventArgs.Empty);

                customColors = value;
                ChangeBackColor();
                OnCustomColorsChanged(EventArgs.Empty);
            }
        }
        /// <summary>
        /// Gets or sets the palette for the FancyControls.Counter control. 
        /// </summary>
        /// <returns>
        /// A FancyControls.ColorsPalette enumeration value that determines the palette to be used.
        /// </returns> 
        [Category("Appearance")]
        [Description("Gets or sets the palette for the FancyControls.Counter control.")]
        [DefaultValue(ColorsPalette.None)]
        public ColorsPalette Palette
        {
            get
            {
                return palette;
            }
            set
            {
                customColors = null;
                OnCustomColorsChanged(EventArgs.Empty);

                palette = value;
                colorsPalette = ColorsPalettes.getPalette(palette);
                ChangeBackColor();
                OnPaletteChanged(EventArgs.Empty);
            }
        }
        /// <summary>
        /// Gets or sets the foreground color of the control.
        /// </summary>
        /// <returns>
        /// The foreground System.Drawing.Color of the control. The default is the value
        /// of the System.Windows.Forms.Control.DefaultForeColor property.
        /// </returns>
        [Category("Appearance")]
        [Description("Gets or sets the foreground color of the control.")]
        [DefaultValue("White")]
        public override Color ForeColor
        {
            get
            {
                return textColor;
            }
            set
            {
                textColor = value;
                Algorithms.ForEachControl<Label>(this, label => label.ForeColor = textColor);
                base.ForeColor = value;
            }
        }

        // EVENTS
        /// <summary>
        /// Occurs when the value of the FancyControls.Counter.Value property changes.
        /// </summary>
        [Category("Bahavior")]
        [Description("Occurs when the value of the FancyControls.Counter.Value property changes.")]
        public event EventHandler<ValueEventArgs<int>> ValueChanged;
        /// <summary>
        /// Occurs when the value of the FancyControls.Counter.Maximum property changes.
        /// </summary>
        [Category("Bahavior")]
        [Description("Occurs when the value of the FancyControls.Counter.Maximum property changes.")]
        public event EventHandler<ValueEventArgs<int>> MaximumChanged;
        /// <summary>
        /// Occurs when the value of the FancyControls.Counter.Minimum property changes.
        /// </summary>
        [Category("Bahavior")]
        [Description("Occurs when the value of the FancyControls.Counter.Minimum property changes.")]
        public event EventHandler<ValueEventArgs<int>> MinimumChanged;
        /// <summary>
        /// Occurs when the value of the FancyControls.Counter.Step property changes.
        /// </summary>
        [Category("Bahavior")]
        [Description("Occurs when the value of the FancyControls.Counter.Step property changes.")]
        public event EventHandler<ValueEventArgs<int>> StepChanged;

        /// <summary>
        /// Occurs when the value of the FancyControls.Counter.Text property changes.
        /// </summary>
        [Category("Appearance")]
        [Description("Occurs when the value of the FancyControls.Counter.Text property changes.")]
        public new event EventHandler<ValueEventArgs<string>> TextChanged;
        /// <summary>
        /// Occurs when the value of the FancyControls.Separator.Value property changes.
        /// </summary>
        [Category("Appearance")]
        [Description("Occurs when the value of the FancyControls.Counter.Separator property changes.")]
        public event EventHandler<ValueEventArgs<string>> SeparatorChanged;

        /// <summary>
        /// Occurs when the value of the FancyControls.Counter.ValueSize property changes.
        /// </summary>
        [Category("Appearance")]
        [Description("Occurs when the value of the FancyControls.Counter.ValueSize property changes.")]
        public event EventHandler<ValueEventArgs<float>> ValueSizeChanged;
        /// <summary>
        /// Occurs when the value of the FancyControls.Counter.TextSize property changes.
        /// </summary>
        [Category("Appearance")]
        [Description("Occurs when the value of the FancyControls.Counter.TextSize property changes.")]
        public event EventHandler<ValueEventArgs<float>> TextSizeChanged;

        /// <summary>
        /// Occurs when the value of the FancyControls.Counter.ChangeColorPerClick property changes.
        /// </summary>
        [Category("Bahavior")]
        [Description("Occurs when the value of the FancyControls.Counter.ChangeColorPerTick property changes.")]
        public event EventHandler ChangeColorPerTickChanged;
        /// <summary>
        /// Occurs when the value of the FancyControls.Counter.ShowMaxValue property changes.
        /// </summary>
        [Category("Appearance")]
        [Description("Occurs when the value of the FancyControls.Counter.ShowMaxValue property changes.")]
        public event EventHandler ShowMaxValueChanged;



        /// <summary>
        /// Occurs when the value of the FancyControls.Counter.Palette property changes.
        /// </summary>
        [Category("Appearance")]
        [Description("Occurs when the value of the FancyControls.Counter.Palette property changes.")]
        public event EventHandler PaletteChanged;
        /// <summary>
        /// Occurs when the value of the FancyControls.Counter.CustomColors property changes.
        /// </summary>
        [Category("Appearance")]
        [Description("Occurs when the value of the FancyControls.Counter.CustomColors property changes.")]
        public event EventHandler CustomColorsChanged;

        /// <summary>
        /// Occurs with each call to the FancyControls.Counter.Tick() method.
        /// </summary>
        [Category("Action")]
        [Description("Occurs with each call to the FancyControls.Counter.Tick() method.")]
        public event EventHandler TickHappened;
        /// <summary>
        /// Occurs when the control is clicked.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when the control is clicked.")]
        public new event EventHandler Click
        {
            add
            {
                Algorithms.ForEachControl<Control>(this, (control) => control.Click += value);
            }
            remove
            {
                Algorithms.ForEachControl<Control>(this, (control) => control.Click -= value);
            }
        }
        


        // METHODS

        /// <summary>
        /// Advances the current value of the countert to the value of  the FancyControls.Counter.Minimum property.
        /// </summary>
        public void Reset()
        {
            Value = Minimum;
            ChangeBackColor();
        }
        /// <summary>
        /// Advances the current value of the counter by the amount of the FancyControls.Counter.Step property.
        /// </summary>
        /// <exception cref = "System.ArgumentException" >
        /// The new value specified is greater than the value of the FancyControls.Counter.Maximum property.
        /// -or- 
        /// The new value specified is less than the value of the FancyControls.Counter.Minimum property.
        /// </exception>
        public void Tick()
        {
            Value += Step;
            ChangeBackColor();
            OnTick(EventArgs.Empty);
        }
        /// <summary>
        /// Advances the current value of the counter by the specified amount.
        /// </summary>
        /// <param name="value">
        /// The amount by which to increment the counter's value.
        /// </param>
        /// <exception cref = "System.ArgumentException" >
        /// The new value specified is greater than the value of the FancyControls.Counter.Maximum property.
        /// -or- 
        /// The new value specified is less than the value of the FancyControls.Counter.Minimum property.
        /// </exception>
        public void Increment(int value)
        {
            Value += value;
            ChangeBackColor();
        }
        /// <summary>
        /// Gets the value of FancyControls.Counter in percentages.
        /// </summary>
        /// <returns>
        /// The value of FancyControls.Counter in percentages.
        /// </returns>
        public double Percentage()
        {
            return value * 100 / maxValue;
        }
        /// <summary>
        /// Rounds the value of the FancyControls.Counter.Value property to its nearest possible value if it is needed.
        /// </summary>
        public void CorrectValue()
        {
            if(!IsValueInRange(Value))
            {
                if(value > maxValue)
                {
                    Value = Maximum;
                }
                else
                {
                    Value = Minimum;
                }
            }
        }
        // determine is value in range of minimum and maximum properties
        private bool IsValueInRange(int value)
        {
            return value >= minValue && value <= maxValue;
        }
        private bool ValidCustomColors()
        {
            return customColors != null && customColors.Length > 0;
        }
        private void ChangeBackColor()
        {
            if (isColorChangedPerTick)
            {
                if (Palette != ColorsPalette.None)
                {
                    this.BackColor = colorsPalette[Math.Abs(Value) % colorsPalette.Length];
                }
                else if (ValidCustomColors())
                {
                    this.BackColor = customColors[Math.Abs(Value) % customColors.Length];
                }
                else
                {
                    this.BackColor = stdBackColor;
                }
            }
        }

        // EVENTS METHODS 

        /// <summary>
        /// Raises the FancyControls.Counter.TickHappened event.
        /// </summary>
        /// <param name="e">
        /// A System.EventArgs that contains the event data.
        /// </param>
        protected void OnTick(EventArgs e)
        {
            TickHappened?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the FancyControls.Counter.ValueChanged event.
        /// </summary>
        /// <param name="e">
        /// A FancyControls.ValueEventArgs that contains the event data.
        /// </param>
        protected void OnValueChanged(ValueEventArgs<int> e)
        {
            countLbl.Text = e.NewValue.ToString();
            countLbl.Font = new Font(countLbl.Font.FontFamily, valueFontSize);
            Algorithms.FitTextByWidth(countLbl);
            ValueChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the FancyControls.Counter.OnMaximumChanged event.
        /// </summary>
        /// <param name="e">
        /// A FancyControls.ValueEventArgs that contains the event data.
        /// </param>
        protected void OnMaximumChanged(ValueEventArgs<int> e)
        {
            CorrectValue();
            maxLbl.Text = e.NewValue.ToString();
            MaximumChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the FancyControls.Counter.MinimumChanged event.
        /// </summary>
        /// <param name="e">
        /// A FancyControls.ValueEventArgs that contains the event data.
        /// </param>
        protected void OnMinimumChanged(ValueEventArgs<int> e)
        {
            CorrectValue();
            Value = e.NewValue;
            MinimumChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the FancyControls.Counter.ValueChanged event.
        /// </summary>
        /// <param name="e">
        /// A FancyControls.ValueEventArgs that contains the event data.
        /// </param>
        protected void OnStepChanged(ValueEventArgs<int> e)
        {
            StepChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the FancyControls.Counter.ValueChanged event.
        /// </summary>
        /// <param name="e">
        /// A FancyControls.ValueEventArgs that contains the event data.
        /// </param>
        protected void OnTextChanged(ValueEventArgs<string> e)
        {
            textLbl.Text = text;
            textLbl.Font = new Font(textLbl.Font.FontFamily, textFontSize);
            Algorithms.FitTextByWidth(textLbl);
            TextChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the FancyControls.Counter.ValueChanged event.
        /// </summary>
        /// <param name="e">
        /// A FancyControls.ValueEventArgs that contains the event data.
        /// </param>
        protected void OnSeparatorChanged(ValueEventArgs<string> e)
        {
            separatorLbl.Text = e.NewValue;
            SeparatorChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the FancyControls.Counter.ValueChanged event.
        /// </summary>
        /// <param name="e">
        /// A FancyControls.ValueEventArgs that contains the event data.
        /// </param>
        protected void OnValueSizeChanged(ValueEventArgs<float> e)
        {
            countLbl.Font = new Font(countLbl.Font.FontFamily, e.NewValue);
            ValueSizeChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the FancyControls.Counter.ValueChanged event.
        /// </summary>
        /// <param name="e">
        /// A FancyControls.ValueEventArgs that contains the event data.
        /// </param>
        protected void OnTextSizeChanged(ValueEventArgs<float> e)
        {
            textLbl.Font = new Font(textLbl.Font.FontFamily, e.NewValue);
            TextSizeChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the FancyControls.Counter.ValueChanged event.
        /// </summary>
        /// <param name="e">
        /// A System.EventArgs that contains the event data.
        /// </param>
        protected void OnShowMaxValueChanged(EventArgs e)
        {
            topFlp.Visible = isMaxValueShowed;
            ShowMaxValueChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the FancyControls.Counter.ChangeColorPerTickChanged event.
        /// </summary>
        /// <param name="e">
        /// A System.EventArgs that contains the event data.
        /// </param>
        protected void OnChangeColorPerTickChanged(EventArgs e)
        {
            ChangeColorPerTickChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the FancyControls.Counter.PaletteChanged event.
        /// </summary>
        /// <param name="e">
        /// A System.EventArgs that contains the event data.
        /// </param>
        protected void OnPaletteChanged(EventArgs e)
        {
            
            PaletteChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the FancyControls.Counter.ValueChanged event.
        /// </summary>
        /// <param name="e">
        /// A System.EventArgs that contains the event data.
        /// </param>
        protected void OnCustomColorsChanged(EventArgs e)
        {
            CustomColorsChanged?.Invoke(this, e);
        }

        // if Control change cursor all Lable change cursor too
        // because user hover on label, not on Control
        private void Counter_CursorChanged(object sender, EventArgs e)
        {
            Algorithms.ForEachControl<Control>(this, control => control.Cursor = this.Cursor);
        }

        private void Counter_Resize(object sender, EventArgs e)
        {
            countLbl.Font = new Font(countLbl.Font.FontFamily, valueFontSize);
            Algorithms.FitTextByWidth(countLbl);

            textLbl.Font = new Font(textLbl.Font.FontFamily, textFontSize);
            Algorithms.FitTextByWidth(textLbl);
        }
    }
}
