using System.Collections.Generic;

public class SecuenceGenerationAndStorage 
{
    private List<string> SecuenceNames = new List<string>();
    public void AddToSecuence(string _elementName)
    {
        SecuenceNames.Add(_elementName);
    }
    public List<string> GetSecuences()
    {
        return SecuenceNames;
    }
    public void OnResetGame()
    {
        SecuenceNames.Clear();
    }

}
