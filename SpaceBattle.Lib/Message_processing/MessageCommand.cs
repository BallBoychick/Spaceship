using Hwdtech;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SpaceBattle.Lib;

public class MessageCommand : ICommand
{
    private string queue;

    public MessageCommand(string queue)
    {
        this.queue = queue;
    }
    public void Execute()
    {
        imessage = queue.Take();
        IoC.Resolve<ICommand>("Send", ThreadID, SthCommand(param)());
    }
}