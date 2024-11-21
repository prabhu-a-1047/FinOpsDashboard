using FinOps_Dashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace FinOps_Dashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            //List<VirtualMachine> vmList = new List<VirtualMachine>();
            List<Resource_VM> resourceList = new List<Resource_VM>();
            
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiURL = "https://dashboardvmmicroserviceapi.azure-api.net/api/VirtualMachine/GetResource_VM";
                    using (var httpResponse = await httpClient.GetAsync(apiURL))
                    {

                        string strResponse = await httpResponse.Content.ReadAsStringAsync();

                        resourceList = JsonConvert.DeserializeObject<List<Resource_VM>>(strResponse);


                        ViewBag.ResourceList = resourceList.Select( s => new { name = s.name }).Distinct().ToList(); 

                        List<GraphData> graphData = new List<GraphData>();

                        //locationDisplayName  resourceGroupName
                        graphData = resourceList.GroupBy(item => item.name)
                                    .Select(group => new GraphData
                                    {
                                        //locationDisplayName = resourceList.First().locationDisplayName,
                                        name = group.Key.ToString(),
                                        Id = group.Count()
                                    })
                                          .ToList();


                        return View(graphData);

                    }

                }

                //using (var httpClient = new HttpClient())
                //{
                //    string apiURL = "https://dashboardvmmicroserviceapi.azure-api.net/api/VirtualMachine/GetResource_VM";
                //    using (var httpResponse = await httpClient.GetAsync(apiURL))
                //    {

                //        string strResponse = await httpResponse.Content.ReadAsStringAsync();

                //        resourceList = JsonConvert.DeserializeObject<List<Resource_VM>>(strResponse);


                //        List<GraphData> graphData = new List<GraphData>();
                //        //locationDisplayName  resourceGroupName
                //        graphData = resourceList.GroupBy(item => item.name)
                //                    .Select(group => new GraphData
                //                    {
                //                        //locationDisplayName = resourceList.First().locationDisplayName,
                //                        name = group.Key.ToString(),
                //                        Id = group.Count()
                //                    })
                //                          .ToList();


                //        return View(graphData);

                //    }

                //}

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }            

        }

        [HttpPost]
        public async Task<JsonResult> Get_Graph(string SelectedOption)
        {

            //List<VirtualMachine> vmList = new List<VirtualMachine>();
            List<Resource_VM> resourceList = new List<Resource_VM>();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiURL = "https://dashboardvmmicroserviceapi.azure-api.net/api/VirtualMachine/GetResource_VM";
                    

                    using (var httpResponse =await  httpClient.GetAsync(apiURL))
                    {

                        string strResponse = await httpResponse.Content.ReadAsStringAsync();

                        resourceList = JsonConvert.DeserializeObject<List<Resource_VM>>(strResponse);


                        //ViewBag.ResourceList = resourceList.Select(s => new { name = s.name }).Distinct().ToList();

                        List<GraphData> graphData = new List<GraphData>();

                        //locationDisplayName  resourceGroupName
                        graphData = resourceList.Where(item => item.name == SelectedOption).GroupBy(item => item.name)
                                    .Select(group => new GraphData
                                    {
                                        //locationDisplayName = resourceList.First().locationDisplayName,
                                        name = group.Key.ToString(),
                                        Id = group.Count()
                                    })
                                          .ToList();

                        
                        return Json(new { success = true, data = graphData });
                    }

                }
                
                //using (var httpClient = new HttpClient())
                //{
                //    string apiURL = "https://dashboardvmmicroserviceapi.azure-api.net/api/VirtualMachine/GetResource_VM";
                //    using (var httpResponse = await httpClient.GetAsync(apiURL))
                //    {

                //        string strResponse = await httpResponse.Content.ReadAsStringAsync();

                //        resourceList = JsonConvert.DeserializeObject<List<Resource_VM>>(strResponse);


                //        List<GraphData> graphData = new List<GraphData>();
                //        //locationDisplayName  resourceGroupName
                //        graphData = resourceList.GroupBy(item => item.name)
                //                    .Select(group => new GraphData
                //                    {
                //                        //locationDisplayName = resourceList.First().locationDisplayName,
                //                        name = group.Key.ToString(),
                //                        Id = group.Count()
                //                    })
                //                          .ToList();


                //        return View(graphData);

                //    }

                //}

            }
            catch (Exception ex)
            {                
                return new JsonResult(ex.Message);
            }

        }


        [HttpGet]
        public async Task<IActionResult> Privacy()
        {

            List<VirtualMachine> vmList = new List<VirtualMachine>();            

            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiURL = "https://dashboardvmmicroserviceapi.azure-api.net/api/VirtualMachine/GetVirtualMachine";
                    using (var httpResponse = await httpClient.GetAsync(apiURL))
                    {

                        string strResponse = await httpResponse.Content.ReadAsStringAsync();

                        vmList = JsonConvert.DeserializeObject<List<VirtualMachine>>(strResponse);

                    }                  
                   

                }

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

            return View(vmList);

        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}