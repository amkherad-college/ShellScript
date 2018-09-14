﻿using System;
using ShellScript.CommandLine;
using ShellScript.Core;
using ShellScript.Core.Language;
using ShellScript.Unix.Bash;

namespace ShellScript
{
    class Program
    {
        public const int Successful = 0;
        public const int Failure = 1;

        static int Main(string[] args)
        {
            var outputWriter = Console.Out; //new StreamWriter(Console.OpenStandardError());
            var errorWriter = new ColoredWriter(Console.Out, ConsoleColor.Red); //new StreamWriter(Console.OpenStandardError());
            var warningWriter = new ColoredWriter(Console.Out, ConsoleColor.Yellow);
            var logWriter = new ColoredWriter(Console.Out, ConsoleColor.White);

            try
            {
                var commandContext = CommandContext.Parse(args);

                Platforms.AddPlatform(new UnixBashPlatform());

                foreach (var command in ApplicationContext.AvailableCommands)
                {
                    if (command.CanHandle(commandContext))
                    {
                        return command.Execute(outputWriter, errorWriter, warningWriter, logWriter, commandContext);
                    }
                }
            }
            catch (Exception ex)
            {
                errorWriter.WriteLine(ex);
            }

            errorWriter.WriteLine(DesignGuidelines.ErrorOutputHead + " Invalid command passed. ({0})", args.Length > 0 ? args[0] : "null");
            return Failure;
        }
    }
}