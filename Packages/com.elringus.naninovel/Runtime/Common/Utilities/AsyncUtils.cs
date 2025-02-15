namespace Naninovel
{
    public static class AsyncUtils
    {
        public static UniTask.Awaiter GetAwaiter (this UniTask? task)
        {
            return task?.GetAwaiter() ?? UniTask.CompletedTask.GetAwaiter();
        }

        public static UniTask<T>.Awaiter GetAwaiter<T> (this UniTask<T>? task)
        {
            return task?.GetAwaiter() ?? UniTask.FromResult<T>(default).GetAwaiter();
        }

        /// <summary>
        /// Waits till the end of the current update loop. Doesn't allocate on heap.
        /// </summary>
        public static YieldAwaitable WaitEndOfFrame (AsyncToken token = default)
        {
            // "LastPostLateUpdate" causes issues with rendering, using the one before.
            return UniTask.Yield(PlayerLoopTiming.PostLateUpdate, token);
        }

        public static async UniTask DelayFrame (int frameCount, AsyncToken token = default)
        {
            await UniTask.DelayFrame(frameCount);
            token.ThrowIfCanceled();
        }
    }
}
