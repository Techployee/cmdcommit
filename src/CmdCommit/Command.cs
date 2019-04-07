using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CmdCommit.Commands
{
    public class Command
    {
        public string Class { get; set; }
        public string Function { get; set; }

        public IDictionary<string, object> Parameters { get; set; }


        public void Invoke()
        {
            Type type = Type.GetType($"CmdCommit.Commands.{ Class }Command");
            MethodInfo method = type.GetMethod(Function);
            var newObject = Activator.CreateInstance(type);
            method.Invoke(newObject, GetParameters(method.GetParameters()));
        }

        private object[] GetParameters(ParameterInfo[] getParameters)
        {
            IList<object> parameters = new List<object>();
            foreach (ParameterInfo parameterInfo in getParameters)
            {
                if (Parameters.ContainsKey(parameterInfo.Name))
                {
                    parameters.Add(Parameters[parameterInfo.Name]);
                }
                else
                {
                    if (parameterInfo.ParameterType == typeof(string))
                    {
                        parameters.Add(null);
                    }
                    else
                    {
                        parameters.Add(Activator.CreateInstance(parameterInfo.ParameterType));
                    }
                }
            }

            return parameters.ToArray();
        }
    }
}
