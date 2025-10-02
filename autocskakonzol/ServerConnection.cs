using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace autocskakonzol
{
    internal class ServerConnection
    {
        HttpClient clinet = new HttpClient();
        string baseUrl;

        public ServerConnection(string url)
        {
            if (!url.StartsWith("http://")) throw new ArgumentException("Hibas url");

            baseUrl = url;
        }

        public async Task<List<Car>> GetCars()
        {
            List<Car> resutl = new List<Car>();

            string url = baseUrl + "/api/cars";

            try
            {
                HttpResponseMessage response = await clinet.GetAsync(url);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                resutl = JsonSerializer.Deserialize<List<Car>>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return resutl;
        }

        public async Task<Message> PostCars(string makeId, string model, int performance, int year, int tireSize)
        {
            Message mesage = new Message();

            string url = baseUrl + "/api/cars";

            try
            {
                var jsonData = new
                {
                    manufacturerID = makeId,
                    modell = model,
                    performance = performance,
                    manufacturerYear = year,
                    wheelSize = tireSize
                };
                string jsonStr = JsonSerializer.Serialize(jsonData);

                HttpContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await clinet.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                mesage = JsonSerializer.Deserialize<Message>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return mesage;
        }

        public async Task<Message> DeleteCars(string id)
        {
            Message mesage = new Message();

            string url = baseUrl + "/api/cars/" + id;

            try
            {
                HttpResponseMessage response = await clinet.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                mesage = JsonSerializer.Deserialize<Message>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
            }

            return mesage;
        }

        public async Task<List<Manufacturer>> GetManufacturers()
        {
            List<Manufacturer> resultt = new List<Manufacturer>();

            string url = baseUrl + "/api/manufacturer";

            try
            {
                HttpResponseMessage response = await clinet.GetAsync(url);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                resultt = JsonSerializer.Deserialize<List<Manufacturer>>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return resultt;
        }

        public async Task<Message> PostManufacturers(string name, int fYear, string country, int mYear)
        {
            Message msg = new Message();

            string url = baseUrl + "/api/manufacturer";

            try
            {

                var jsonData = new
                {
                    name = name,
                    foundationYear = fYear,
                    country = country,
                    manufacturerYear = mYear
                };
                string jsonStr = JsonSerializer.Serialize(jsonData);
                HttpContent content = new StringContent(jsonStr, Encoding.UTF8, "Application/json");
                HttpResponseMessage response = await clinet.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                msg = JsonSerializer.Deserialize<Message>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return msg;
        }

        public async Task<Message> DeleteManufacturers(string id)
        {
            Message mesg = new Message();

            string url = baseUrl + "/api/manufacturer/" + id;

            try
            {
                HttpResponseMessage response = await clinet.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                mesg = JsonSerializer.Deserialize<Message>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
            }

            return mesg;
        }

        public async Task<List<Owner>> GetOwners()
        {
            List<Owner> rslts = new List<Owner>();

            string url = baseUrl + "/api/owner";

            try
            {
                HttpResponseMessage response = await clinet.GetAsync(url);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                rslts = JsonSerializer.Deserialize<List<Owner>>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return rslts;
        }

        public async Task<Message> PostOwners(string carId, string name, string address, int bYear)
        {
            Message mssg = new Message();

            string url = baseUrl + "/api/owner";

            try
            {
                var jsonData = new
                {
                    carID = carId,
                    name = name,
                    address = address,
                    birthYear = bYear
                };
                string jsonStr = JsonSerializer.Serialize(jsonData);
                HttpContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await clinet.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                mssg = JsonSerializer.Deserialize<Message>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return mssg;
        }

        public async Task<Message> DeleteOwners(string id)
        {
            Message mssg = new Message();

            string url = baseUrl + "/api/owner/" + id;

            try
            {

                HttpResponseMessage response = await clinet.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                mssg = JsonSerializer.Deserialize<Message>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
            }

            return mssg;
        }
    }
}
