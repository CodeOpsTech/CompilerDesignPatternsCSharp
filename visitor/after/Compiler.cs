// This code uses visitor pattern to cleanly separate the concerns 

using System;
using System.Collections.Generic;

internal abstract class Expr
{
    public abstract void Accept(Visitor v);
}

internal class Constant : Expr
{
    private int _val;

    public Constant(int arg)
    {
        _val = arg;
    }

    public virtual int Val => _val;

    public override void Accept(Visitor v)
    {
        v.Visit(this);
    }
}

internal class Plus : Expr
{
    public Plus(Expr arg1, Expr arg2)
    {
        Left = arg1;
        Right = arg2;
    }

    public virtual Expr Left { get; }

    public virtual Expr Right { get; }

    public override void Accept(Visitor v)
    {
        v.Visit(this);
    }
}

internal class Sub : Expr
{
    public Sub(Expr arg1, Expr arg2)
    {
        Left = arg1;
        Right = arg2;
    }

    public virtual Expr Left { get; }

    public virtual Expr Right { get; }

    public override void Accept(Visitor v)
    {
        v.Visit(this);
    }
}

internal abstract class Visitor
{
    public abstract void Visit(Constant constant);
    public abstract void Visit(Plus plus);
    public abstract void Visit(Sub sub);
    public abstract void GenCode(Expr expr);
}

internal class JvmVisitor : Visitor
{
    public override void Visit(Constant arg)
    {
        Console.WriteLine("iload " + arg.Val);
    }

    public override void Visit(Plus plus)
    {
        GenCode(plus.Left);
        GenCode(plus.Right);
        Console.WriteLine("iadd");
    }

    public override void Visit(Sub sub)
    {
        GenCode(sub.Left);
        GenCode(sub.Right);
        Console.WriteLine("isub");
    }

    public override void GenCode(Expr expr)
    {
        expr.Accept(this);
    }
}

internal class DotnetVisitor : Visitor
{
    public override void Visit(Constant arg)
    {
        Console.WriteLine("ldarg " + arg.Val);
    }

    public override void Visit(Plus plus)
    {
        GenCode(plus.Left);
        GenCode(plus.Right);
        Console.WriteLine("add");
    }

    public override void Visit(Sub sub)
    {
        GenCode(sub.Left);
        GenCode(sub.Right);
        Console.WriteLine("sub");
    }

    public override void GenCode(Expr expr)
    {
        expr.Accept(this);
    }
}

internal class ExprEvalVisitor
{
    public static void Main(string[] args)
    {
        // (10 + (20 - 30))  
        Expr expr = new Plus(new Constant(10), new Sub(new Constant(20), new Constant(30)));
        //Visitor v = new DotnetVisitor(); // JVMVisitor();
        Visitor v = new JvmVisitor(); // JVMVisitor();
        v.GenCode(expr);
        Console.ReadLine();
    }
}
