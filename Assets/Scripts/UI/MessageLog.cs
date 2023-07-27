using CommonMethodsLibrary;

public class MessageLog : TextManager
{
    private void Start()
    {
        txtObj.gameObject.SetActive(false);
    }

    protected override void ShowMessage(string message)
    {
        base.ShowMessage(message);

        txtObj.gameObject.SetActive(true);

        Invoke("HideMessage", timeToHideMessage);
    }

    void HideMessage()
    {
        txtObj.gameObject.SetActive(false);
    }


}
