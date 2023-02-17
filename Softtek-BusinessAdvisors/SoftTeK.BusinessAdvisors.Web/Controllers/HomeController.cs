using Microsoft.AspNetCore.Mvc;
using SoftTeK.BusinessAdvisors.Dto.Users;
using SoftTeK.BusinessAdvisors.Web.Constants;
using SoftTeK.BusinessAdvisors.Web.Models;
using SoftTeK.BusinessAdvisors.Web.Repository;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;

namespace SoftTeK.BusinessAdvisors.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository _repository;
        private readonly HttpClient httpClient;

        public HomeController(ILogger<HomeController> logger, IRepository repository, HttpClient httpClient)
        {
            _logger = logger;
            _repository = repository;
            this.httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            ISession _session = HttpContext.Session;
            string json = _session.GetString(UserSession.Key);
            string token = json != null ? JsonSerializer.Deserialize<UserDto>(json).Token : null;

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            var httpResponse = await _repository.Get<List<UserDto>>($"api/Users");
            IndexModel model = new IndexModel();
            model.Users = httpResponse.Response;

            model.User = json != null ? JsonSerializer.Deserialize<UserDto>(json) : null;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            IndexModel indexModel = new IndexModel();

            var httpResponse = await _repository.Post<UserLoginDto, UserDto>($"api/Users/login", model);

            if (httpResponse.Error)
            {
                var mensajeError = await httpResponse.GetBody();

                return RedirectToAction("Index", "Home", indexModel);
            }
            else
            {
                var mensajeError = await httpResponse.GetBody();
                var response = httpResponse.Response;

                ISession _session = HttpContext.Session;
                _session.SetString(UserSession.Key, JsonSerializer.Serialize(response));

                return RedirectToAction("Index", "Home", indexModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto model)
        {
            IndexModel indexModel = new IndexModel();

            ISession _session = HttpContext.Session;
            string json = _session.GetString(UserSession.Key);
            string token = JsonSerializer.Deserialize<UserDto>(json).Token;

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            var httpResponse = await _repository.Post<UserDto>($"api/Users", model);

            if (httpResponse.Error)
            {
                var mensajeError = await httpResponse.GetBody();

                return RedirectToAction("Index", "Home", indexModel);
            }
            else
            {
                var httpResponseUsers = await _repository.Get<List<UserDto>>($"api/Users");
                indexModel.Users = httpResponseUsers.Response;

                return RedirectToAction("Index", "Home", indexModel);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }


}