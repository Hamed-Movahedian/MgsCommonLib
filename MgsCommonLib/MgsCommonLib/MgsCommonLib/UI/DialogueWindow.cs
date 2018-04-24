using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using MgsCommonLib.Utilities;

public class DialogueWindow : MonoBehaviour
{

    #region Actions

    private Dictionary<string, Action> _actionDic =
        new Dictionary<string, Action>();


    public void RunAction(string name)
    {
        if (_actionDic.ContainsKey(name))
            _actionDic[name]();
    }

    public void SetAction(string lable, Action action)
    {
        if (_actionDic.ContainsKey(lable))
            _actionDic[lable] = action;
        else
            _actionDic.Add(lable, action);
    }

    #endregion

    #region InputFields

    public string this[string name]
    {
        get
        {
            InputField inputField = GetInputField(name);

            if (inputField)
                return inputField.text;

            return "";
        }
        set
        {
            InputField inputField = GetInputField(name);

            if (inputField)
                inputField.text = value;
        }
    }

    private InputField[] _inputFields;
    private InputField GetInputField(string name)
    {
        if(_inputFields==null)
            _inputFields = GetComponentsInChildren<InputField>(true);

        foreach (var inputField in _inputFields)
        {
            if (inputField.name.ToLower().Trim() == name.ToLower().Trim())
                return inputField;
        }

        Debug.LogError("Input field " + name + " not found!!");

        return null;

    }

    #endregion

    #region Get Window

    public static DialogueWindow GetWindow(string name)
    {
        var window = FindObjectsOfType<DialogueWindow>()
            .FirstOrDefault(w => w.name == name);

        if (window != null)
            return window;

        Debug.LogError("Dialogue window " + name + " not found!!");

        return null;

    }


    #endregion

    #region ShowWindowAndWaitForClose

    private bool _isDone = false;

    public IEnumerator ShowWindowAndWaitForClose()
    {
        transform.SetActiveChilds(true);

        _isDone = false;

        while (!_isDone)
            yield return null;

        transform.SetActiveChilds(false);

        yield return null;
    }

    public void Complete()
    {
        _isDone = true;
    }
    #endregion

}

