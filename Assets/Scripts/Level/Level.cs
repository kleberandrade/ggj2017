using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

	[SerializeField]
	private TextAsset m_Level;

    [SerializeField]
    private GameObject m_RobotPrefab;

    [SerializeField]
    private GameObject[] m_EnemiesPrefab;

    [SerializeField]
    private GameObject m_EventPrefab;

    [SerializeField]
    private GameObject m_Portal;

    [SerializeField]
    private GameObject[] m_ItemsPrefab;

    private int width;
	private int height;

	public bool ready = false;

	public void LoadLevel () {
        ready = false;
		string[] lines = m_Level.text.Replace("\r", "").Split ('\n');

		string label = null;

        //GameObject robot = Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/Player/Robot.prefab", typeof(GameObject))) as GameObject;


        GameObject robot = Instantiate(m_RobotPrefab) as GameObject;
        for (int l = 0; l < lines.Length; ++l) {
			if (label == null) {
				label = lines[l];
			} else {
				if (label.StartsWith ("Level:")) {
					string sheet = lines [l];
					l++;
					string old;
					string current = lines [l].Replace ('\t', ' ');
					do {
						old = current;
						current = old.Replace ("  ", " ");
					} while (old != current);
					string[] sizes = current.Trim().Split (' ');
					int rows = int.Parse (sizes [0]);
					int cols = int.Parse (sizes [1]);
					width = int.Parse (sizes [2]);
					height = int.Parse (sizes [3]);
					int spriteRows = int.Parse (sizes [4]);
					int spriteCols = int.Parse (sizes [5]);
					l++;
					List<Sprite> sprites = new List<Sprite> ();


                    //Texture2D texture = UnityEditor.AssetDatabase.LoadAssetAtPath ("Assets/Sprites/Level/" + sheet + ".png", typeof(Texture2D)) as Texture2D;
                    Texture2D texture = Resources.Load<Texture2D>(sheet);
                    Debug.Log(texture);

                    for (int r = 0; r < spriteRows; ++r) {
						for (int c = 0; c < spriteCols; ++c) {
							sprites.Add (Sprite.Create (texture, new Rect(new Vector2(c * width, texture.height - (r + 1) * height), new Vector2(width, height)), new Vector2(0.5f,0.5f)));
						}
					}
					for (int r = 0; r < rows; ++r) {
						string[] tiles = lines [l].Split (';');
						for (int c = 0; c < cols; ++c) {
							string[] tile = tiles [c].Split (',');
							GameObject go = new GameObject ();
							go.transform.parent = transform;
							go.transform.Translate(new Vector3(c * width / 100.0f, - r * height / 100.0f, 0));
							go.transform.Rotate (new Vector3 (0, 0, float.Parse(tile [2])));
							SpriteRenderer sr = go.AddComponent <SpriteRenderer>() as SpriteRenderer;
							sr.sprite = sprites[int.Parse(tile[1])];
							switch (int.Parse(tile [0].Trim ())) {
							case 1:
								// Obstáculo
								BoxCollider2D bc2 = go.AddComponent <BoxCollider2D>() as BoxCollider2D;
								bc2.size = new Vector2 (width / 100.0f, height / 100.0f);
								break;
							case 2:
								// Top/Left
								PolygonCollider2D pc2 = go.AddComponent<PolygonCollider2D> () as PolygonCollider2D;
								pc2.points = new Vector2[] {
									new Vector2 (0, 0),
									new Vector2 (width / 100.0f, 0),
									new Vector2 (0, - height / 100.0f)
								};
								pc2.offset = new Vector2 (-width / 200.0f, height / 200.0f);
								break;
							case 3:
								// Top/Right
								pc2 = go.AddComponent<PolygonCollider2D> () as PolygonCollider2D;
								pc2.points = new Vector2[] {
									new Vector2 (0, 0),
									new Vector2 (width / 100.0f, 0),
									new Vector2 (width / 100.0f, - height / 100.0f)
								};
								pc2.offset = new Vector2 (-width / 200.0f, height / 200.0f);
								break;
							case 4:
								// Top/Left
								pc2 = go.AddComponent<PolygonCollider2D> () as PolygonCollider2D;
								pc2.points = new Vector2[] {
									new Vector2 (0, 0),
									new Vector2 (width / 100.0f, - height / 100.0f),
									new Vector2 (0, - height / 100.0f)
								};
								pc2.offset = new Vector2 (-width / 200.0f, height / 200.0f);
								break;
							case 5:
								// Top/Right
								pc2 = go.AddComponent<PolygonCollider2D> () as PolygonCollider2D;
								pc2.points = new Vector2[] {
									new Vector2 (0, - height / 100.0f),
									new Vector2 (width / 100.0f, 0),
									new Vector2 (width / 100.0f, - height / 100.0f)
								};
								pc2.offset = new Vector2 (-width / 200.0f, height / 200.0f);
								break;
							default:
								break;
							}
						}
						++l;
					}
				} else if (label.StartsWith ("StartPortals:")) {
					int num = int.Parse (lines [l]);
					List<int> x = new List<int>();
					List<int> y = new List<int>();
					while (num > 0) {
						++l;
						string[] coord = lines [l].Replace ("\t", " ").Split (' ') [0].Split (',');
						x.Add (int.Parse (coord [0]));
						y.Add (int.Parse (coord [1]));
                        Debug.Log("X: " + coord[0] + ",Y: " + coord[1]);
						--num;
					}
					int p = Random.Range (0, x.Count);
                    Debug.Log("P: " + p);
                    //robot.transform.parent = transform;
                    robot.transform.position = new Vector3(x[p] * width / 100.0f, - y[p] * height / 100.0f, 0);

				} else if (label.StartsWith ("EndPortals:")) {
					int num = int.Parse (lines [l]);
					List<int> x = new List<int>();
					List<int> y = new List<int>();
					while (num > 0) {
						++l;
						string[] coord = lines [l].Replace ("\t", " ").Split (' ') [0].Split (',');
						x.Add (int.Parse (coord [0]));
						y.Add (int.Parse (coord [1]));
						--num;
					}

                    //Object prefab = UnityEditor.AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/Player/Portal.prefab", typeof(GameObject));
                    Object prefab = m_Portal;
                    for (int p = 0; p < x.Count; ++p) {
						GameObject portal = Instantiate (prefab) as GameObject;
						portal.transform.position = new Vector3 (x [p] * width / 100.0f, -y [p] * height / 100.0f, 0);
					}
				} else if (label.StartsWith ("Enemies:")) {
					int num = int.Parse (lines [l]);
					List<int> enemy = new List<int> ();
					List<int> x = new List<int>();
					List<int> y = new List<int>();
					while (num > 0) {
						++l;
						string[] coord = lines [l].Replace ("\t", " ").Split (' ') [0].Split (',');
						enemy.Add (int.Parse (coord [0]));
						x.Add (int.Parse (coord [1]));
						y.Add (int.Parse (coord [2]));
						--num;
					}

                    //Object prefab1 = UnityEditor.AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/Enemies/Enemy01L.prefab", typeof(GameObject));
                    //Object prefab2 = UnityEditor.AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/Enemies/Enemy2.prefab", typeof(GameObject));
                    Object prefab1 = m_EnemiesPrefab[0];
                    Object prefab2 = m_EnemiesPrefab[1];
                    for (int p = 0; p < x.Count; ++p) {
						GameObject portal = Instantiate (enemy[p] == 0 ? prefab1 : prefab2) as GameObject;
						AStar astar = portal.GetComponent<AStar> ();
						astar.Target = robot;
						portal.transform.position = new Vector3 (x [p] * width / 100.0f, -y [p] * height / 100.0f, 0);
						Vector3 pos = portal.transform.position;
						pos.z = -0.5f;
						portal.transform.position = pos;
					}
				} else if (label.StartsWith ("Items:")) {
					int num = int.Parse (lines [l]);
					List<int> item = new List<int> ();
					List<int> x = new List<int>();
					List<int> y = new List<int>();
					while (num > 0) {
						++l;
						string[] coord = lines [l].Replace ("\t", " ").Split (' ') [0].Split (',');
						item.Add (int.Parse (coord [0]));
						x.Add (int.Parse (coord [1]));
						y.Add (int.Parse (coord [2]));
						--num;
					}


                    //Object prefab1 = UnityEditor.AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/Item/Item1.prefab", typeof(GameObject));
                    //Object prefab2 = UnityEditor.AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/Item/Item2.prefab", typeof(GameObject));

                    Object prefab1 = m_ItemsPrefab[0];
                    Object prefab2 = m_ItemsPrefab[1];
                    for (int p = 0; p < x.Count; ++p) {
						GameObject portal = Instantiate (item[p] == 0 ? prefab1 : prefab2) as GameObject;
						portal.transform.Translate (new Vector3 (x [p] * width / 100.0f, -y [p] * height / 100.0f, 0));
					}
				} else if (label.StartsWith ("Dialogs:")) {
					int num = int.Parse (lines [l]);
					while (num > 0) {
						++l;
						string[] parts = lines [l].Split (new char[] { ':' }, 2);
						DialogManager.Instance.Register(new Dialog(int.Parse (parts [0]), parts [1]));
						--num;
					}
				} else if (label.StartsWith ("Events:")) {
					int num = int.Parse (lines [l]);
					++l;
                    Object prefab = Instantiate(m_EventPrefab);
					while (num > 0) {
						string[] pars = lines [l].Split (',');
						int type = int.Parse (pars [0]);
						int dialogNum = int.Parse (pars [1]);
						int x = int.Parse (pars [2]);
						int y = int.Parse (pars [3]);
						int r = int.Parse (pars [4]);
						List<Dialog> dialogues = new List<Dialog> ();
						while (dialogNum > 0) {
							++l;
							string[] parts = lines [l].Split (new char[] { ':' }, 2);
							dialogues.Add (new Dialog (int.Parse (parts [0]), parts [1]));
							--dialogNum;
						}

						GameObject portal = Instantiate (prefab) as GameObject;
						portal.transform.position = new Vector3 (x * width / 100.0f - (r - 1) / 2, -y * height / 100.0f - (r - 1) / 2, 0);
						portal.transform.localScale = new Vector3 (r, r, 1);
						portal.GetComponent<Event> ().dialogues = dialogues;
						--num;
					}
				}
				label = null;
			}
		}
		ready = true;
	}

}
