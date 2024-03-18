using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using IronXL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net;
using static System.Net.Mime.MediaTypeNames;


namespace VFXWebsite.Pages
{
    public class MainPageModel : PageModel
    {
        [BindProperty]
        public ForexDailyPrice? ForexDaily { get; set; } //Information necessary to construct the URL below

        public List<SelectListItem> AllCurrencies { get; set; } = new List<SelectListItem>();  //List with all the currencies that will be loaded below

        public string open { get; set; }
        public string high { get; set; }
        public string low { get; set; }
        public string close { get; set; }
        public string TimeRefreshed { get; set; } 

   

        public void OnGet()
        {
            LoadList();
            //LoadListHardCoded();
        }

        public void OnPost()
        {
         string QUERY_URL = "https://www.alphavantage.co/query?function=FX_DAILY&from_symbol="+ ForexDaily.From +"&to_symbol=" + ForexDaily.To + "&apikey=6CZ0007BTGAKMZZP"; // URL to do the request to the API

            Uri queryUri = new Uri(QUERY_URL);

            using (WebClient client = new WebClient())
            {
                JObject jsonObj = JObject.Parse(client.DownloadString(queryUri)); //Gets the information as a JSON from the API 
                if (jsonObj != null)
                {
                    var forexDaily = (JObject)jsonObj["Time Series FX (Daily)"]; 
                    if(forexDaily!= null)
                    {
                        var fistForexInfo = forexDaily.First; 
                        if(fistForexInfo != null)
                        {
                            var infoToTheTable = fistForexInfo.First;//Gets the first date info
                            if(infoToTheTable != null)
                            {
                                open = (string)infoToTheTable["1. open"];
                                high = (string)infoToTheTable["2. high"];
                                low = (string)infoToTheTable["3. low"];
                                close = (string)infoToTheTable["4. close"];
                            }         
                        }
                        
                    }           
                }
            }

            TimeRefreshed = DateTime.Now.ToString();

            LoadList();
            //LoadListHardCoded(); 
        }

        private void LoadList()
        {
            string fileName = "E:\\projetos\\VFXWebsite\\VFXWebsite\\wwwroot\\excel\\currencies.xlsx"; //This is the path in my computer for the folder "excel" and the file "currencies.xlsx"

            WorkBook workBook = WorkBook.Load(fileName); //Opens the excel File
            WorkSheet workSheet = workBook.WorkSheets.First(); //Opens the first sheet of the excel
     
            foreach(var cell in workSheet["A2:A121"]) //reads the cells on the A collumn
            {
               
                AllCurrencies.Add(new SelectListItem { Value = cell.ToString(), Text = cell.ToString() }); //adds the cell info to the list that is loaded on the DropDownList in the front end
            }   
        }

        private void LoadListHardCoded() //In case the LoadList does not work please use this hardcoded list with a few currencies
        {
            AllCurrencies = new List<SelectListItem> {
        new SelectListItem { Value = "EUR", Text = "EUR" },
        new SelectListItem { Value = "USD", Text = "USD" },
        new SelectListItem { Value = "KZT", Text = "KZT" },
        new SelectListItem { Value = "GNF", Text = "GNF" }};
        }

    }

   
  

    public class ForexDailyPrice
    {
        [Required]
        public string From { get; set; }
        [Required]
        public string To { get; set; }
        
    }

    
}
