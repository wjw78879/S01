[System.Serializable]
public class SaveData
{
    public int gemnum;
    public int levelNo;
    public float playerX, playerY, playerZ;
    public int progress;

    public SaveData(int gem, int level, float posX, float posY, float posZ, int prog)
    {
        gemnum = gem;
        levelNo = level;
        playerX = posX;
        playerY = posY;
        playerZ = posZ;
        progress = prog;
    }
}
