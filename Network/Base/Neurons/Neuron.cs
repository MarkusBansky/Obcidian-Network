namespace Network.Base.Neurons
{
    public class Neuron
    {
        public readonly string Guid;
        public double Value;

        public Neuron()
        {
            Guid = System.Guid.NewGuid().ToString();
        }

        public override bool Equals(object obj)
        {
            Neuron neuron = obj as Neuron;
            return neuron != null && (obj.GetType() == typeof(Neuron) && neuron.Guid.Equals(Guid));
        }

        protected bool Equals(Neuron other)
        {
            return string.Equals(Guid, other.Guid) && Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Guid?.GetHashCode() ?? 0)*397) ^ Value.GetHashCode();
            }
        }
    }
}
