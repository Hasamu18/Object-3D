using Assimp;
using HelixToolkit.Wpf;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using SharpDX;
using System.Windows.Shapes;
using HelixToolkit.SharpDX.Core.Assimp;
using gs;
namespace Convert3DObject.Views
{
    /// <summary>
    /// Interaction logic for _3DModelView.xaml
    /// </summary>
    public partial class _3DModelView : Window
    {
        public Model3D _model;
        public string _path;
        Scene _3Dscene = null;
        ModelVisual3D modelVisual = new ModelVisual3D();
        public _3DModelView(string path)
        {
            
            InitializeComponent();
            ModelImporter import = new ModelImporter();

            _model = import.Load(path);
            _path = path;

            var scene = new AssimpContext();
            _3Dscene = scene.ImportFile(_path,PostProcessPreset.TargetRealTimeMaximumQuality | PostProcessSteps.FixInFacingNormals | PostProcessSteps.ForceGenerateNormals);
            DisplayModel();
            MetricCalculate();
            CompositionTarget.Rendering += OnRendering;
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            this.Owner.Show();
        }

        public void DisplayModel()
        {
            
            
            modelVisual.Content = _model;

            viewPort3d.Children.Add(modelVisual);
             
            viewPort3d.Camera.UpDirection = new System.Windows.Media.Media3D.Vector3D(0, 1, 0);
            viewPort3d.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 192, 192, 192));

        }
        public void MetricCalculate()
        {

            var meshes = new MeshGeometry3D();
            
            // Access the mesh data
            foreach (var mesh in _3Dscene.Meshes)
            {
                // Add the vertices to the MeshGeometry3D object
                foreach (var vertex in mesh.Vertices)
                {
                    meshes.Positions.Add(new Point3D(vertex.X, vertex.Y, vertex.Z));
                }

                // Add the triangle indices to the MeshGeometry3D object
                foreach (var face in mesh.Faces)
                {
                    foreach (var index in face.Indices)
                    {
                        meshes.TriangleIndices.Add(index);
                    }
                }

            }
           
        
            MeshCount.Text = "Vertices Count : " + meshes.Positions.Count.ToString();
            Triangle_Count.Text = "Triangle Count : " + meshes.TriangleIndices.Count / 3;
            SizeX.Text = "Size X : " + meshes.Bounds.SizeX.ToString("F2");/*meshes.Bounds.SizeX.ToString("F2")*/;
            SizeY.Text = "Size Y : " + meshes.Bounds.SizeY.ToString("F2");
            SizeZ.Text = "Size Z : " + meshes.Bounds.SizeZ.ToString("F2");

            
            
        }
          private void OnRendering(object sender, EventArgs e)
        {
            if (viewPort3d.Camera is PerspectiveCamera camera)
            {
                var position = camera.Position;
                var lookDirection = camera.LookDirection;
                var upDirection = camera.UpDirection;

                // Display the camera's position and orientation
                Rotation.Text = $"Position: {position}, LookDirection: {lookDirection}, UpDirection: {upDirection}";
            }
        }
      


    }
}
