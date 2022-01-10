using AngleSharp.Parser.Html;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace OMDB_kursova
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Search_Button(object sender, RoutedEventArgs e)
        {
           //Making it so the results dont stack one after another
            movTitle.Text = "Title: ";
            movYear.Text = "Year: ";
            movRated.Text = "Rated: ";
            movReleased.Text = "Released: ";
            movRuntime.Text = "Runtime: ";
            movGenre.Text = "Genre: ";
            movDirector.Text = "Director: ";
            movWriter.Text = "Writer: ";
            movActor.Text = "Actors: ";
            movPlot.Text = "Plot: ";
            movLang.Text = "Language: ";
            movCountry.Text = "Country: ";
            movAwards.Text = "Awards: ";
            movImdbRating.Text = "IMDB Rating: ";
            movSeasons.Text = "Seasons: ";
            movPoster.Source = null;


            // Creating a new http client
            var client = new HttpClient();

            // Taking the movie title from the text box
            string title = movieSearch.Text;

            //Getting the movie info from the API
            try
            {
                var json = await client.GetStringAsync(new Uri("http://www.omdbapi.com/?t=" + title + "&apikey=6484425e"));
                // Deserializing the json information
                var mov = JsonConvert.DeserializeObject<RootObject>(json);


                // If the title is not found the response will be false and the title will be set to Titanic


                //if (Convert.ToBoolean(mov.Response) == false || (title == "" || title == "not available"))
                //{
                //    title = "Titanic";
                //    json = await client.GetStringAsync(new Uri("http://www.omdbapi.com/?t=" + title + "&apikey=6484425e"));
                //    mov = JsonConvert.DeserializeObject<RootObject>(json);
                //}

                // Parsing the json to html to get the image path string
                var Poster = new HtmlParser().Parse(mov.Poster);
                string posterPath = Poster.Body.TextContent;

                // Using BitmapImage get the image from the provided Uri
                BitmapImage imageBitmap = new BitmapImage(new Uri(posterPath));
                //Assigning the value of the BitmapImage to the Xaml image box
                movPoster.Source = imageBitmap;

                //Assigning the information from Omdb to the text fields in xaml.

                movTitle.Text += mov.Title;

                movYear.Text += mov.Year;

                movRated.Text += mov.Rated;

                movReleased.Text += mov.Released;

                movRuntime.Text += mov.Runtime;

                movGenre.Text += mov.Genre;

                movDirector.Text += mov.Director;

                movWriter.Text += mov.Writer;

                movActor.Text += mov.Actors;

                movPlot.Text += mov.Plot;

                movLang.Text += mov.Language;

                movCountry.Text += mov.Country;

                movAwards.Text += mov.Awards;

                movImdbRating.Text += mov.imdbRating;

                movSeasons.Text += mov.totalSeasons;
            }
            catch
            {
                movieSearch.Text = "The title was not found or you have no connection.";
            }

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            movieSearch.Text = "";
            movTitle.Text = "Title: ";
            movYear.Text = "Year: ";
            movRated.Text = "Rated: ";
            movReleased.Text = "Released: ";
            movRuntime.Text = "Runtime: ";
            movGenre.Text = "Genre: ";
            movDirector.Text = "Director: ";
            movWriter.Text = "Writer: ";
            movActor.Text = "Actors: ";
            movPlot.Text = "Plot: ";
            movLang.Text = "Language: ";
            movCountry.Text = "Country: ";
            movAwards.Text = "Awards: ";
            movImdbRating.Text = "IMDB Rating: ";
            movSeasons.Text = "Seasons: ";
            movPoster.Source = null;
        }

        //
        private void movieSearch_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Search_Button(sender, e);
            }
        }
    }
}
