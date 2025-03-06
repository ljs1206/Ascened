namespace Game.Core
{
public class SpawnManagerL : MonoSingleton<SpawnManagerL>
{
    // [SerializeField] private SpawnTable _spawnTable;
    //
    // private readonly string _SoSavePath = "Assets/00.Work/LJS/06.SO/Spawns";
    //
    // public event Action EnemyAllDeadEvent;
    //
    // [HideInInspector] public int _LeftEnmeyCount;
    //
    // private void Awake(){
    //     if(_spawnTable == null) 
    //     {
    //         SpawnTable spawnTable = new SpawnTable();
    //         AssetDatabase.CreateAsset(spawnTable, $"{_SoSavePath}/spawnTable.asset");
    //         _spawnTable = AssetDatabase.LoadAssetAtPath<SpawnTable>($"{_SoSavePath}/spawnTable.asset");
    //         Debug.LogWarning("Made SpawnTable. if End PlayMode please you '_spawnTable' set SO");
    //     }   
    //
    //     _spawnTable._spawnList = FindObjectsByType<Enemy>(FindObjectsSortMode.None).ToList();
    //     _LeftEnmeyCount = _spawnTable._spawnList.Count;
    // }
    //
    // public void DeleteObject(Enemy delObj){
    //     if(_spawnTable._spawnList.Count > 0 && _spawnTable._spawnList.Find(x =>  x == delObj)){
    //         _spawnTable._spawnList.Remove(delObj);
    //         _LeftEnmeyCount--;
    //     }
    //     
    //     if(_spawnTable._spawnList.Count <= 0){
    //         EnemyAllDeadEvent?.Invoke();
    //     }
    // }
    //
    // public void AddObject(Enemy addObj){
    //     _spawnTable._spawnList.Add(addObj);
    // }
    //
    // public void ChangeSpawnTable(SpawnTable spawnTable){
    //     _spawnTable._spawnList.Clear();
    //     _LeftEnmeyCount = spawnTable._spawnList.Count;
    //     _spawnTable = spawnTable;
    // }
    //
    // private void OnDestroy(){
    //     _spawnTable._spawnList.Clear();
    // }
}
}
