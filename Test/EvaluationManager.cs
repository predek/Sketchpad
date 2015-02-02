using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCalc;

namespace Sketchpad
{
    public class EvaluationManager
    {
        MainWindow window;

        public EvaluationManager(MainWindow window)
        {
            this.window = window;
        }

        public void update()
        {
            //called after node/
        }


        public String evaluate(String expressionString, Dictionary<String, String> parametersDictionary)
        {
            String value = "";

            if (expressionString == "")
            {
                return value;
            }

            try
            {
                NCalc.Expression expression = new Expression(expressionString);

                if (parametersDictionary.Count > 0)
                {
                    foreach (KeyValuePair<string, string> pair in parametersDictionary)
                    {
                        expression.Parameters[pair.Key] = Convert.ToDouble(pair.Value);
                    }
                }

                try
                {
                    value = expression.Evaluate().ToString();
                }
                catch (EvaluationException exception)
                {

                }

            }
            catch (ArgumentException exception)
            {
                return value;
            }
            catch (FormatException exception)
            {
                return value;
            }

            return value;
        }
    }
}
