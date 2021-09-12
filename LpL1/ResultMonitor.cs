using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LpL1.Data;

namespace LpL1
{
    public class ResultMonitor
    {
        private int _lastIndex = 0;
        
        private readonly int _size;
        private ProcessedVehicle[] Data { get; set; }

        public ResultMonitor(int size)
        {
            _size = size;
            Data = new ProcessedVehicle[_size];
        }

        public void Add(ProcessedVehicle processedVehicle)
        {
            Data[_lastIndex] = processedVehicle;
                        _lastIndex++;
            // var index = GetIndexToAdd();
            //
            // var newData = new ProcessedVehicle[_size];
            //
            // for (int i = 0; i < index; i++)
            // {
            //     newData[i] = Data[i];
            // }
            //
            // newData[index] = processedVehicle;
            //
            // for (int i = index; i < _size; i++)
            // {
            //     newData[i+1] = Data[i];
            // }
        }

        private int GetIndexToAdd()
        {
            throw new NotImplementedException("pirk subarika");
        }

        public void WriteToFile(string filePath)
        {
            using var streamWriter = new StreamWriter(filePath);
            
            foreach (var item in Data)
            {
                streamWriter.WriteLine(item.ToString());
            }
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder = Data
                .Aggregate(stringBuilder,
                    (current, vehicle) => current.AppendLine($"{vehicle.Model}")
                    );

            return stringBuilder.ToString();
        }
        
        // public override 
    }
}