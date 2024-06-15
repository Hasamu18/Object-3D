using System.Windows;
using System.Windows.Media.Media3D;

namespace Convert3DObject
{
    /// <summary>
    /// Interaction logic for _3DModelPreview.xaml
    /// </summary>
    public partial class _3DModelPreview : Window
    {
        public Model3D _model;

        public _3DModelPreview(Model3D model)
        {
            InitializeComponent();
            _model = model;
            DisplayModel();
        }

        public void DisplayModel()
        {
            ModelVisual3D modelVisual = new ModelVisual3D();
            modelVisual.Content = _model;

            viewPort3d.Children.Add(modelVisual);
            viewPort3d.Camera.UpDirection = new Vector3D(0, 1, 0);

        }

    }
}
