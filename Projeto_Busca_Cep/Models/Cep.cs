using Nancy.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Projeto_Busca_Cep.Models
{
    public class Cep
    {
        [Display(Name = "Cep ")]
        public string CEP { get; set; }

        [Display(Name = "Logradouro ")]
        public string Logradouro { get; set; }

        [Display(Name = "Bairro ")]
        public string Bairro { get; set; }

        [Display(Name = "Localidade ")]
        public string Localidade { get; set; }

        [Display(Name = "UF ")]
        public string UF { get; set; }

        [Display(Name = "DDD ")]
        public string DDD { get; set; }

        public static Cep Busca(string cep)
        {
            var cepObj = new Cep();

            var url = "https://viacep.com.br/ws/" + cep + "/json/";

            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                string json = string.Empty;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }
                JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                JsonCepObject cepJson = json_serializer.Deserialize<JsonCepObject>(json);

                cepObj.CEP = cepJson.cep;
                cepObj.Logradouro = cepJson.logradouro;
                cepObj.Bairro = cepJson.bairro;
                cepObj.Localidade = cepJson.localidade;
                cepObj.UF = cepJson.uf;
                cepObj.DDD = cepJson.ddd;
            }
            catch (Exception e)
            {
                new Exception($"{e}");
            }
            return cepObj;
        }

        public class JsonCepObject
        {
            public string cep { get; set; }
            public string logradouro { get; set; }
            public string bairro { get; set; }
            public string localidade { get; set; }
            public string uf { get; set; }
            public string ddd { get; set; }
        }

    }
}
