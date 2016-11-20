using UnityEngine;
using System.Collections;

public interface IBlockerState : IEnemyState {

    void ToBlockingState();
}
