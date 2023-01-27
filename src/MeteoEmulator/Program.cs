using System;
using System.Threading;
using System.Threading.Tasks;
using MeteoEmulator.Helpers;

namespace MeteoEmulator
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Arguments processedArgs;

            try
            {
                processedArgs = Arguments.FromArgs(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();

                return;
            }
            
            Console.WriteLine("Starting emulator...");

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            Task.Factory.StartNew(() => { StartAction(processedArgs.Url, processedArgs.SleepInterval, processedArgs.InstanceID, processedArgs.DataStability, processedArgs.WithNoise, token); }, token);

            Console.ReadKey();

            Console.WriteLine("Key pressed. Exit...");
            tokenSource.Cancel();
        }

        public static async void StartAction(string baseUrl, int sleepInterval, string id, int dataStability, bool makeNoise, CancellationToken token)
        {
            var generator = new DataGenerator(id, makeNoise, dataStability);
            var sender = new DataSender();

            var dataUrl = baseUrl.TrimEnd('/') + "/data";
            var noiseDataUrl = baseUrl.TrimEnd('/') + "/noiseData";

            while (!token.IsCancellationRequested)
            {
                generator.UpdateSensorData();

                var data = generator.GetData();

                if (data.DataPackageID % sleepInterval == 0)
                {
                    var noiseData = generator.GetDataWithNoise();
                    
                    Console.WriteLine("Sending data...");

                    var dataSent = await sender.Send(data, dataUrl, token);
                    var noiseDataSent = await sender.Send(noiseData, noiseDataUrl, token);

                    Console.WriteLine(dataSent && noiseDataSent ? "Data sent successfully" : "Unable to send data");
                }

                Thread.Sleep(1000);
            }
        }
    }
}