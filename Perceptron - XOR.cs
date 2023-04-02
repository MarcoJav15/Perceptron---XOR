//Marco Javier de León Vasquez 1521719
//Perceptron - XOR

using System;
namespace BackPropagationXor
{
    class principal
    {
        static void Main(string[] args)
        {
            train();
        }

        class modificar
        {
            public static double prod(double x)
            {
                return 1.0 / (1.0 + Math.Exp(-x));
            }

            public static double der(double x)
            {
                return x * (1 - x);
            }
        }
        //Declaracion neurona
        class Neurona
        {
            public double[] inputs = new double[2];
            public double[] pesos = new double[2];
            public double error;

            private double biasWeight;

            private Random r = new Random();

            public double prod
            {
                get { return modificar.prod(pesos[0] * inputs[0] + pesos[1] * inputs[1] + biasWeight); }
            }

            public void peso_random()
            {
                pesos[0] = r.NextDouble();
                pesos[1] = r.NextDouble();
                biasWeight = r.NextDouble();
            }

            public void ajuste_pesos()
            {
                pesos[0] += error * inputs[0];
                pesos[1] += error * inputs[1];
                biasWeight += error;
            }
        }

        private static void train()
        {
            
            double[,] inputs =
            {
                { 0, 0},
                { 0, 1},
                { 1, 0},
                { 1, 1}
            };

          
            double[] resultados = { 0, 1, 1, 0 };

           //Neuronas
            Neurona n1 = new Neurona();
            Neurona n2 = new Neurona();
            Neurona pruebaNeurona = new Neurona();
            n1.peso_random();
            n2.peso_random();
            pruebaNeurona.peso_random();

            int num = 0;

        Retry:
            num++;
            //Entrenamiento
            for (int i = 0; i < 4; i++) 
            {
                n1.inputs = new double[] { inputs[i, 0], inputs[i, 1] };
                n2.inputs = new double[] { inputs[i, 0], inputs[i, 1] };
                pruebaNeurona.inputs = new double[] { n1.prod, n2.prod };
                Console.WriteLine("{0} xor {1} = {2}", inputs[i, 0], inputs[i, 1], pruebaNeurona.prod);
                pruebaNeurona.error = modificar.der(pruebaNeurona.prod) * (resultados[i] - pruebaNeurona.prod);
                pruebaNeurona.ajuste_pesos();
                n1.error = modificar.der(n1.prod) * pruebaNeurona.error * pruebaNeurona.pesos[0];
                n2.error = modificar.der(n2.prod) * pruebaNeurona.error * pruebaNeurona.pesos[1];
                n1.ajuste_pesos();
                n2.ajuste_pesos();
            }

            if (num < 2000)
                goto Retry;
            Console.ReadLine();
        }
    }
}