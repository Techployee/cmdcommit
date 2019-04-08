using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CmdCommit
{
    public class Command
    {

        public string Class { get; set; }
        public string Function { get; set; }

        public IDictionary<string, object> Parameters { get; set; }

       
        /// <summary>
        /// Invoke the method with parameters.
        /// </summary>
        public void Invoke()
        {
            // Get the class.
            Type type = Type.GetType($"CmdCommit.Commands.{ Class }Command");

            // Get the method.
            MethodInfo method = type.GetMethod(Function);

            // Create the class.
            var newObject = Activator.CreateInstance(type);

            // Invoke the method using parameters.
            method.Invoke(newObject, GetParameters(method.GetParameters()));
        }

        /// <summary>
        /// Gets the method parameters to inject and return a object array.
        /// </summary>
        /// <param name="methodParameters"></param>
        /// <returns></returns>
        private object[] GetParameters(ParameterInfo[] methodParameters)
        {
            IList<object> parameters = new List<object>();
            foreach (ParameterInfo parameterInfo in methodParameters)
            {
                if (Parameters.ContainsKey(parameterInfo.Name))
                {
                    parameters.Add(Parameters[parameterInfo.Name]);
                }
                else
                {
                    parameters.Add(parameterInfo.ParameterType == typeof(string)
                        ? null
                        : Activator.CreateInstance(parameterInfo.ParameterType));
                }
            }
            return parameters.ToArray();
        }

        
    }
}
