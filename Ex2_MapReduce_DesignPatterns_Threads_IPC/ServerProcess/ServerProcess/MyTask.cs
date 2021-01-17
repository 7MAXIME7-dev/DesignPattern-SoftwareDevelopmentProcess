using System;

namespace Communication
{
    [Serializable]
    class MyTask
    {
        public string taskName;
        public object inputData;
        public object outputData;

        public MyTask(string taskName, object inputData)
        {
            this.taskName = taskName;
            this.inputData = inputData;
            this.outputData = null;
        }


        public override string ToString()
        {
            return String.Format("Task.info | Name:{0} | Input:{1} | Output:{2}", taskName, inputData, outputData);
        }

    }
}
