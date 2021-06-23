using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;


namespace GPSTracker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        GMarkerGoogle marker;
        GMapOverlay overlay;
        static double LatInicial = 13.69;
        static double LongInicial = -89.19;

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Descripcion", typeof(string)));
            dt.Columns.Add(new DataColumn("Lat", typeof(double)));
            dt.Columns.Add(new DataColumn("Long", typeof(double)));

            dt.Rows.Add("1", LatInicial, LongInicial);
            data.DataSource = dt;

            //Initial Settings
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(13.69, -89.19);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.AutoScroll = true;
            gMapControl1.Zoom = 12;

            //Marker & Overlay
            marker = new GMarkerGoogle(new PointLatLng(LatInicial, LongInicial), GMarkerGoogleType.blue);
            overlay = new GMapOverlay();
            overlay.Markers.Add(marker); //agregando el marker al overlay
            //ToolTip for stetics
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            marker.ToolTipText = string.Format("This Point Location is nutssssss ");

            //Add the previous step to GMAP control
            gMapControl1.Overlays.Add(overlay);

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtLat.Text == "" || txtLong.Text == "" )
            {
                MessageBox.Show("Faltan datos pelotudo");
            }
            else
            {
                double lat = Convert.ToDouble(txtLat.Text);
                double longi = Convert.ToDouble(txtLong.Text);

                newPoint(lat, longi);
            }

        }

        private void newRoute()
        {


        }

        private void newPoint(double lat, double longi)
        {

            marker.Position = new PointLatLng(lat, longi);

        }

        private void gMapControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            double lat = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
            double lng = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;

            
           marker.Position = new PointLatLng(lat, lng);

            marker.ToolTipText = string.Format("Current Point Location is lat: {0} \n long: {1}", lat, lng);

            txtLat.Text = lat.ToString();
            txtLong.Text = lng.ToString();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void btnRoute_Click(object sender, EventArgs e)
        {
            newRoute();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
