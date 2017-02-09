using Newtonsoft.Json;

namespace ObsidiaDebugger.Helpers
{
    internal class ResponseParser
    {
        public static ResponseStructure Parse(string data)
        {
            ResponseStructure response = new ResponseStructure();
            dynamic json = JsonConvert.DeserializeObject(data);

            foreach (var item in json.neurons)
            {
                response.Neurons.Add(new Neuron
                {
                    Id = item.id,
                    InputValue = item.input,
                    OutputValue = item.output
                });
            }
            foreach (var item in json.connections)
            {
                response.Connections.Add (new Connection
                {
                    ToIndex = item.to,
                    FromIndex = item.from,
                    Weight = item.weight
                });
            }

            response.InputsCount = json.inputs;
            response.BiasesCount = json.biases;
            response.ComputationalCount = json.computational;
            response.OutputsCount = json.outputs;

            return response;
        }
    }
}