using System;
using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;
public enum NamMessageType
{
    /// <summary>
    /// When the game is over
    /// </summary>
    OnGameOver,
}
public class Message
{
    public NamMessageType type;
    public object[] data;
    public Message(NamMessageType type)
    {
        this.type = type;
    }
    public Message(NamMessageType type, object[] data)
    {
        this.type = type;
        this.data = data;
    }
}
public interface IMessageHandle
{
    void Handle(Message message);
}
public class MessageManager : Singleton<MessageManager>, ISerializationCallbackReceiver
{
    private static MessageManager instance = null;
    
    //Stores information when Serialize data in the subcribers-Dictionary
    [HideInInspector] public List<NamMessageType> _keys = new List<NamMessageType>();
    [HideInInspector] public List<List<IMessageHandle>> _values = new List<List<IMessageHandle>>();
    
    
    private Dictionary<NamMessageType, List<IMessageHandle>> subcribers = new Dictionary<NamMessageType, List<IMessageHandle>>();
    /*public static MessageManager Instance { get { return instance; } }
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }*/
    public void AddSubcriber(NamMessageType type, IMessageHandle handle)
    {
        if (!subcribers.ContainsKey(type))
            subcribers[type] = new List<IMessageHandle>();
        if (!subcribers[type].Contains(handle))
            subcribers[type].Add(handle);
    }
    public void RemoveSubcriber(NamMessageType type, IMessageHandle handle)
    {
        if (subcribers.ContainsKey(type))
            if (subcribers[type].Contains(handle))
                subcribers[type].Remove(handle);
    }
    public void SendMessage(Message message)
    {
        if (subcribers.ContainsKey(message.type))
            for (int i = subcribers[message.type].Count - 1; i > -1; i--)
                subcribers[message.type][i].Handle(message);
    }
    public void SendMessageWithDelay(Message message, float delay)
    {
        StartCoroutine(_DelaySendMessage(message, delay));
    }
    private IEnumerator _DelaySendMessage(Message message, float delay)
    {
        yield return new WaitForSeconds(delay);
        SendMessage(message);
    }
    public void OnBeforeSerialize()
    {
        _keys.Clear();
        _values.Clear();
        foreach (var element in subcribers)
        {
            _keys.Add(element.Key);
            _values.Add(element.Value);
        }
    }
    public void OnAfterDeserialize()
    {
        subcribers = new Dictionary<NamMessageType, List<IMessageHandle>>();
        for (int i = 0; i < _keys.Count; i++)
        {
            subcribers.Add(_keys[i], _values[i]);
        }
    }
}