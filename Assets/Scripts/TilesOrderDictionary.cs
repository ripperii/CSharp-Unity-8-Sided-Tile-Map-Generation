using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TilesOrderDictionary {

    public Dictionary<int, int[]> order = new Dictionary<int, int[]>();

    public TilesOrderDictionary()
    {
        order.Add(-1, new int[4] { findId(21, 9), findId(22, 9), findId(21, 10), findId(22, 10) });
        order.Add(0,  new int[4] { findId(0, 0), findId(1, 0), findId(0, 1), findId(1, 1) });
        order.Add(1,  new int[4] { findId(0, 4), findId(3, 4), findId(0, 5), findId(3, 5) });
        order.Add(2,  new int[4] { findId(2, 2), findId(3, 2), findId(2, 5), findId(3, 5) });
        order.Add(3,  new int[4] { findId(2, 4), findId(3, 4), findId(2, 5), findId(3, 5) });
        order.Add(4,  new int[4] { findId(0, 2), findId(1, 2), findId(0, 5), findId(1, 5) });
        order.Add(5,  new int[4] { findId(0, 4), findId(1, 4), findId(0, 5), findId(1, 5) });
        order.Add(6,  new int[4] { findId(1, 2), findId(2, 2), findId(1, 5), findId(2, 5) });
        order.Add(7,  new int[4] { findId(1, 4), findId(2, 4), findId(1, 5), findId(2, 5) });
        order.Add(8,  new int[4] { findId(0, 2), findId(3, 2), findId(0, 3), findId(3, 3) });
        order.Add(9,  new int[4] { findId(0, 3), findId(3, 3), findId(0, 4), findId(3, 4) });
        order.Add(10, new int[4] { findId(2, 2), findId(3, 2), findId(2, 3), findId(3, 3) });
        order.Add(11, new int[4] { findId(2, 3), findId(3, 3), findId(2, 4), findId(3, 4) });
        order.Add(12, new int[4] { findId(0, 2), findId(1, 2), findId(0, 3), findId(1, 3) });
        order.Add(13, new int[4] { findId(0, 3), findId(1, 3), findId(0, 4), findId(1, 4) });
        order.Add(14, new int[4] { findId(1, 2), findId(2, 2), findId(1, 3), findId(2, 3) });
        order.Add(15, new int[4] { findId(1, 3), findId(2, 3), findId(1, 4), findId(2, 4) });
    }
    public int findId(int c, int r)
    {
        return (r+18) * 32 + c;
    }
}
