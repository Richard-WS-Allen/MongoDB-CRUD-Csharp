using MongoDB.Driver;
using System;
using System.Linq;
using System.Windows.Forms;

namespace mongo_crud
{
    public partial class Form1 : Form
    {
        // Forward declarations
        double rating;
        double slope;

        // MongoDB Client, Database, and Collection
        static MongoClient client = new MongoClient();
        static IMongoDatabase db = client.GetDatabase("golfDb");
        static IMongoCollection<Course> courseCollection = db.GetCollection<Course>("courses");


        public Form1()
        {
            // Display Form
            InitializeComponent();            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void searchButtonClick(object sender, EventArgs e)
        {
            // Filter object to find Course by name
            var filter = Builders<Course>.Filter.Eq("CourseName", nameTextBox.Text);
            
            // Find course and display data. Display message if not found.
            try
            {
                var foundCourse = courseCollection.Find(filter).First();
                ratingTextBox.Text = $"{foundCourse.Rating.ToString("0.00")}";
                slopeTextBox.Text = $"{foundCourse.Slope.ToString("0.00")}";
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("No Results Found.");
            }


        }

        private void addButtonClick(object sender, EventArgs e)
        {
            // Validate input
            try
            {
                rating = Double.Parse(ratingTextBox.Text);
                slope = Double.Parse(slopeTextBox.Text);
            } catch (FormatException)
            {
                MessageBox.Show("Invalid entry in rating or slope.");
            }
            courseCollection.InsertOne(new Course {
                CourseName = nameTextBox.Text,
                Rating = rating,
                Slope = slope });
        }

        private void updateButtonClick(object sender, EventArgs e)
        {
            // Validate input
            try
            {
                rating = Double.Parse(ratingTextBox.Text);
                slope = Double.Parse(slopeTextBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid entry in rating or slope.");
                return;
            }
            // Filter and update objects
            var filter = Builders<Course>.Filter.Eq("CourseName", nameTextBox.Text);
            var update = Builders<Course>.Update.Set("Rating", rating).Set("Slope", slope);

            // Update collection with above objects
            courseCollection.UpdateOne(filter, update);
        }

        private void deleteButtonClick(object sender, EventArgs e)
        {
            // Delete course by name
            var filter = Builders<Course>.Filter.Eq("CourseName", nameTextBox.Text);
            courseCollection.DeleteOne(filter);
        }
    }
}
