using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmdCommit.Commands;

namespace CmdCommit
{
    public class Setting
    {
        public IDictionary<string, object> Environment { get; set; }
        public IList<Command> Commands { get; set; }


        public Setting(IDictionary<string, object> environment, IList<Command> commands)
        {
            Environment = environment;
            Commands = commands;
            ReplaceVariablesWithEnvironment();
        }

        /// <summary>
        /// Replace sections $(parameter) of the command parameters with environment parameters.
        /// </summary>
        private void ReplaceVariablesWithEnvironment()
        {
            foreach (Command command in Commands)
            {
                foreach (KeyValuePair<string, object> param in command.Parameters.ToList())
                {
                    if (!(param.Value is string newVariable)) continue;

                    foreach (KeyValuePair<string, object> eVar in Environment)
                    {
                        if (newVariable.Contains(eVar.Key))
                        {
                            newVariable = newVariable.Replace(eVar.Key, eVar.Value.ToString());
                        }
                    }
                    command.Parameters[param.Key] = newVariable;
                }
            }
           
        }
    }
}
