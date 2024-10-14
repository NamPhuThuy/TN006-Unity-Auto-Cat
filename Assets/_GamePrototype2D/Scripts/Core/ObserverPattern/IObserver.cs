
//This is the listener
public interface IObserver
{
    /*Type of parameter on OnNotify() can vary based on the requirements of the specific project and
    what data the observers might need
    */
    public virtual void OnNotify(){}
    public virtual void OnNotify(string action){}
    
    //The @ character in C# acts as a verbatim identifier modifier. It tells the compiler to interpret the following word exactly as written, without considering any special keywords or reserved words.
    public virtual void OnNotify(IGameEvent @event){}
    

    
    // public void OnNotify(int id);

    // public void OnNotify(Data eventData);
}