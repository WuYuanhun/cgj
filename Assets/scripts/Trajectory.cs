using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{

    public LineRenderer lineRenderer;

    private Vector2 _startPos;
    private Vector2 _currentPos;

    public Vector2 velocity;

    public Vector2[] _posList;
    public int _pos_index = 0;


    private int _num_points = 100;
    private float DELTATIME = 0.1f;
    private float _max_time = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        _currentPos = _startPos;
        velocity = new Vector2(0.0f, 0.0f);
        _posList = new Vector2[_num_points];
        _pos_index = 0;
        _posList[_pos_index] = _startPos;

        // calculate the trajectory
        calculate_list_pos();
        
        // show
        show();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void calculate_list_pos()
    {
        int max_samples = Mathf.Min(_num_points, (int)(_max_time / DELTATIME));
        for(int i = 0; i < max_samples; i++) {
            calculate_velocity();
            calculate_position();
        }
    }

    // calculate the position of the trajectory
    public void calculate_position()
    {
        _pos_index += 1;
        _currentPos = _currentPos + velocity * Time.deltaTime;
        _posList[_pos_index] = _currentPos;
    }

    // aciqure all mass body in the scene and calculate the trajectory
    public void calculate_velocity() {
        Vector2 total_force = new Vector2(0.0f, 0.0f);
        foreach (Planet_Attraction planet in GameManager.Instance.planet_Attractions)
        {
            total_force += planet.getForce(_currentPos);
        }
        velocity = velocity + total_force * DELTATIME;
    }

    public void show()
    {
        Vector3[] line_pos = new Vector3[_posList.Length];
        for (int i = 0; i < _posList.Length; i++)
        {
            line_pos[i] = _posList[i];
        }

        lineRenderer.positionCount = _posList.Length;
        lineRenderer.SetPositions(line_pos);
    }
}
