﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;

namespace Multicore.Negocio
{
    class prueba
    {

        public static void M()
        {
            

            double[] array = new double[20 * 1000 * 1000];

            for (int i = 0; i < array.Length; i++)
                array[i] = 1;

            for (int i = 0; i < 5; i++)
            {
                Stopwatch sw = Stopwatch.StartNew();
                Serial(array, 2);
                Console.WriteLine("Serial: {0:f2} s", sw.Elapsed.TotalSeconds);

                sw = Stopwatch.StartNew();
                ParallelFor(array, 2);
                Console.WriteLine("Parallel.For: {0:f2} s", sw.Elapsed.TotalSeconds);

                sw = Stopwatch.StartNew();
                CustomParallelExtractedMaxHalfParallelism(array, 2);
                Console.WriteLine("Custom parallel (extracted max, half parallelism): {0:f2} s", sw.Elapsed.TotalSeconds);

                /*sw = Stopwatch.StartNew();
                ParallelForDegreeOfParallelism(array, 2);
                Console.WriteLine("Parallel.For (degree of parallelism): {0:f2} s", sw.Elapsed.TotalSeconds);

                 sw = Stopwatch.StartNew();
                CustomParallel(array, 2);
                Console.WriteLine("Custom parallel: {0:f2} s", sw.Elapsed.TotalSeconds);

                sw = Stopwatch.StartNew();
                CustomParallelExtractedMax(array, 2);
                Console.WriteLine("Custom parallel (extracted max): {0:f2} s", sw.Elapsed.TotalSeconds);

                sw = Stopwatch.StartNew();
                CustomParallelExtractedMaxHalfParallelism(array, 2);
                Console.WriteLine("Custom parallel (extracted max, half parallelism): {0:f2} s", sw.Elapsed.TotalSeconds);

                sw = Stopwatch.StartNew();
                CustomParallelFalseSharing(array, 2);
                Console.WriteLine("Custom parallel (false sharing): {0:f2} s", sw.Elapsed.TotalSeconds);
            */
            }
        }








        static void Serial(double[] array, double factor)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = array[i] * factor;
            }
        }

        static void ParallelFor(double[] array, double factor)
        {
            Parallel.For(
                0, array.Length, i => { array[i] = array[i] * factor; });
        }

        static void ParallelForDegreeOfParallelism(double[] array, double factor)
        {
            Parallel.For(
                0, array.Length, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
                i => { array[i] = array[i] * factor; });
        }

        static void CustomParallel(double[] array, double factor)
        {
            var degreeOfParallelism = Environment.ProcessorCount;

            var tasks = new Task[degreeOfParallelism];

            for (int taskNumber = 0; taskNumber < degreeOfParallelism; taskNumber++)
            {
                // capturing taskNumber in lambda wouldn't work correctly
                int taskNumberCopy = taskNumber;

                tasks[taskNumber] = Task.Factory.StartNew(
                    () =>
                    {
                        for (int i = array.Length * taskNumberCopy / degreeOfParallelism;
                            i < array.Length * (taskNumberCopy + 1) / degreeOfParallelism;
                            i++)
                        {
                            array[i] = array[i] * factor;
                        }
                    });
            }

            Task.WaitAll(tasks);
        }

        static void CustomParallelExtractedMax(double[] array, double factor)
        {
            var degreeOfParallelism = Environment.ProcessorCount;

            var tasks = new Task[degreeOfParallelism];

            for (int taskNumber = 0; taskNumber < degreeOfParallelism; taskNumber++)
            {
                // capturing taskNumber in lambda wouldn't work correctly
                int taskNumberCopy = taskNumber;

                tasks[taskNumber] = Task.Factory.StartNew(
                    () =>
                    {
                        var max = array.Length * (taskNumberCopy + 1) / degreeOfParallelism;
                        for (int i = array.Length * taskNumberCopy / degreeOfParallelism;
                            i < max;
                            i++)
                        {
                            array[i] = array[i] * factor;
                        }
                    });
            }

            Task.WaitAll(tasks);
        }

        static void CustomParallelExtractedMaxHalfParallelism(double[] array, double factor)
        {
            var degreeOfParallelism = Environment.ProcessorCount / 2;

            var tasks = new Task[degreeOfParallelism];

            for (int taskNumber = 0; taskNumber < degreeOfParallelism; taskNumber++)
            {
                // capturing taskNumber in lambda wouldn't work correctly
                int taskNumberCopy = taskNumber;

                tasks[taskNumber] = Task.Factory.StartNew(
                    () =>
                    {
                        var max = array.Length * (taskNumberCopy + 1) / degreeOfParallelism;
                        for (int i = array.Length * taskNumberCopy / degreeOfParallelism;
                            i < max;
                            i++)
                        {
                            array[i] = array[i] * factor;
                        }
                    });
            }

            Task.WaitAll(tasks);
        }

        static void CustomParallelFalseSharing(double[] array, double factor)
        {
            var degreeOfParallelism = Environment.ProcessorCount;

            var tasks = new Task[degreeOfParallelism];

            int i = -1;

            for (int taskNumber = 0; taskNumber < degreeOfParallelism; taskNumber++)
            {
                tasks[taskNumber] = Task.Factory.StartNew(
                    () =>
                    {
                        int j = Interlocked.Increment(ref i);
                        while (j < array.Length)
                        {
                            array[j] = array[j] * factor;
                            j = Interlocked.Increment(ref i);
                        }
                    });
            }

            Task.WaitAll(tasks);
        }
     


    }
}
