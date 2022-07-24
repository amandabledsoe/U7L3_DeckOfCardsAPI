using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using U7L3.Models;

namespace U7L3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ShuffleTheCards() // Same as Generate a New Deck
        {
            //1. First, create an HTTP Client via the Client Factory to enable the ability send ab receive a response to the API:
            HttpClient httpClient = _httpClientFactory.CreateClient();

            //2. Second, find the URL to call the API, and set it as a constant for use later.
            const string apiURLtoShuffleTheCards = "https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1";
            //3. See if you have to send the API anything (we do not in this case.)

            //4. Create a Model that models (haha) the returned response from the API. 
            //See 'ShuffleTheCards.cs'

            //5. Call the API using the GetFromJasonAsync method attached to the HTTP Client. 
            //Dont forget to add GetAwaiter() and GetResult()
            var apiDeckReturnedFromShuffle = httpClient.GetFromJsonAsync<ShuffleTheCards>(apiURLtoShuffleTheCards).GetAwaiter().GetResult();

            //6. Create a new instance of the API Response Model and store the API response you get into that instance.
            var displayShuffledDeck = new ShuffleTheCards();
            displayShuffledDeck = apiDeckReturnedFromShuffle;

            //Finally, pass the API Response Model instance to the view so we can see what what are working with!
            return View(displayShuffledDeck);
        }

        public IActionResult DrawACardAction(string deck_id)
        {
            //1. First, create an HTTP Client via the Client Factory to enable the ability send ab receive a response to the API:
            HttpClient httpClient = _httpClientFactory.CreateClient();

            //2. Second, find the URL to call the API, and set it as a constant for use later.
            //https://deckofcardsapi.com/api/deck/<<deck_id>>/draw/?count=2

            //3. See if you have to send the API anything. In this case, we need to give it a deck_id and a count.
            //For now, since we are setting the amount of cards to draw, we can assign that here and pass it in
            int cardCount = 5;

            //We already pass in a deck_id, so now we just plug them both in and set the URL to a variable instead of a constant.
            string apiURLRequestToDrawACard = $"https://deckofcardsapi.com/api/deck/{deck_id}/draw/?count={cardCount}";


            //4. Create a Model that models (haha) the returned response from the API. 
            //See 'DrawACardAction.cs'

            //5. Call the API using the GetFromJasonAsync method attached to the HTTP Client. 
            //Dont forget to add GetAwaiter() and GetResult()
            var drawACardAPIResponse = httpClient.GetFromJsonAsync<DrawACard>(apiURLRequestToDrawACard).GetAwaiter().GetResult();

            //6. Create a new instance of the API Response Model and store the API response you get into that instance.
            var cardsToDisplay = drawACardAPIResponse;

            //Finally, pass the API Response Model instance to the view so we can see what what are working with!
            return View(cardsToDisplay);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}