using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace Communication
{

    public class DataExchange
    {
        public Stream stream;

        public DataExchange(Stream stream)
        {
            this.stream = stream;
        }


        public void writeStream(MyTask toWrite)
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(this.stream, toWrite);
        }

        public object readStream()
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Binder = new PreMergeToMergedDeserializationBinder();
            object output = formatter.Deserialize(stream);

            return output;
        }

    }
}
