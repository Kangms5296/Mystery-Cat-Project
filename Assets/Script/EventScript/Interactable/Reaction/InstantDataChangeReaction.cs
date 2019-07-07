
public class InstantDataChangeReaction : DelayedReaction
{
    private MainGame instantData;


    public string valueName;
    public bool newValue;

    protected override void ImmediateReaction()
    {
        instantData = FindObjectOfType<MainGame>();

        switch(valueName)
        {
            case "isDay20":
                instantData.isDay20 = newValue;
                break;
            case "isFindBirthClue":
                instantData.isFindBirthClue = newValue;
                break;
            case "isGetEar":
                instantData.isGetEar = newValue;
                break;
            case "isGetHair":
                instantData.isGetHair = newValue;
                break;
            case "isGetSpoon":
                instantData.isGetSpoon = newValue;
                break;
            case "isOnCriminalLine":
                instantData.isOnCriminalLine = newValue;
                break;
            case "isOnLibraryLine":
                instantData.isOnLibraryLine = newValue;
                break;
            case "isCustomerNotDrunken":
                instantData.isCustomerNotDrunken = newValue;
                break;
            case "isPoliceWallRemove":
                instantData.isPoliceWallRemove = newValue;
                break;
            case "isLibraryTempRemove":
                instantData.isLibraryTempRemove = newValue;
                break;
            default:
                break;
        }
    }
}
