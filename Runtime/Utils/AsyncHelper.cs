using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public static class AsyncHelper {
    public static async Task WaitAndDoAsync(Action function, int delayInMilliSeconds) {
        await Task.Delay(delayInMilliSeconds);
        function();
    }

    public static IEnumerator WaitAndDoCouroutine(Action function, float delayInSeconds) {
        yield return new WaitForSeconds(delayInSeconds);
        function();
    }
}
