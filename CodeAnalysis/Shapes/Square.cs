using System.Threading.Tasks;

namespace Shapes
{
    internal class Square
    {
        public int SideLength { get; set; }

        public Task<double> GetAreaAsync()
        {
            return default;
        }
    }
}
