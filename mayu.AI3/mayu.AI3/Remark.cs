using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace mayu.AI3
{
    class Remark
    {
        string ReData;
        public string GetWeather(string idata)
        {
            var MAnalysis = new Morphological_Analysis();
            string[] AnalysisData = MAnalysis.Analysis(idata);
            var City = AnalysisData[0];

            switch(City)
            {
                case "北海道" : City = "016010";break;
                case "青森" : City = "020010";break;
                case "岩手": City = "030010";break;
                case "宮城": City = "040010";break;
                case "秋田": City = "050010";break;
                case "山形": City = "060010"; break;
                case "福島": City = "070010"; break;
                case "茨城": City = "080010"; break;
                case "栃木": City = "090010"; break;
                case "群馬": City = "100010"; break;
                case "埼玉": City = "110010"; break;
                case "千葉": City = "120010"; break;
                case "東京": City = "130010"; break;
                case "神奈川": City = "140010"; break;
                case "新潟": City = "150010"; break;
                case "富山": City = "160010"; break;
                case "石川": City = "170010"; break;
                case "福井": City = "180010"; break;
                case "山梨": City = "190010"; break;
                case "長野": City = "200010"; break;
                case "岐阜": City = "210010"; break;
                case "静岡": City = "220010"; break;
                case "愛知": City = "230010"; break;
                case "三重": City = "240010"; break;
                case "滋賀": City = "250010"; break;
                case "京都": City = "260010"; break;
                case "大阪": City = "270000"; break;
                case "兵庫": City = "280010"; break;
                case "奈良": City = "290010"; break;
                case "和歌山": City = "300010"; break;
                case "鳥取": City = "310010"; break;
                case "島根": City = "320010"; break;
                case "岡山": City = "330010"; break;
                case "広島": City = "340010"; break;
                case "山口": City = "350010"; break;
                case "徳島": City = "360010"; break;
                case "香川": City = "370010"; break;
                case "愛媛": City = "380010"; break;
                case "高知": City = "390010"; break;
                case "福岡": City = "400010"; break;
                case "佐賀": City = "410010"; break;
                case "長崎": City = "420010"; break;
                case "熊本": City = "430010"; break;
                case "大分": City = "440010"; break;
                case "宮崎": City = "450010"; break;
                case "鹿児島": City = "460010"; break;
                case "沖縄": City = "471010"; break;
                default : City = "0";break;
            }
            if (City != "0")
            {
                var URL = "http://weather.livedoor.com/forecast/webservice/json/v1?city=" + City;
                var json = new HttpClient().GetStringAsync(URL).Result;
                var jobj = JObject.Parse(json);

                ReData = (string)((jobj["forecasts"][0]["telop"] as JValue).Value);//Get
            }
            else if (City == "0") { ReData = "すいません、よくわかりませんでした"; }

            return ReData;
        }
    }
}
