using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#if _USE_PCL_TIMER_CUSTOM
namespace Serilog
{
    /// <summary>
    /// Missing from PCL, except if targeting .NET 4.5.1 + Win8.1 + WP8.1
    /// </summary>
    sealed class Timer : CancellationTokenSource
    {
        bool _waitForCallbackBeforeNextPeriod;
        int _millisecondsPeriod;
        int _millisecondsDueTime;
        object _state;
        Action<object> _callback;

        public Timer(Action<object> callback) : this(callback, null, 100, 100, false)
        {
        }

        internal Timer(Action<object> callback, object state, int millisecondsDueTime, int millisecondsPeriod, bool waitForCallbackBeforeNextPeriod = false)
        {
            //Contract.Assert(period == -1, "This stub implementation only supports dueTime.");
            _callback = callback;
            _state = state;
            _millisecondsDueTime = millisecondsDueTime;
            _millisecondsPeriod = millisecondsPeriod;
            _waitForCallbackBeforeNextPeriod = waitForCallbackBeforeNextPeriod;

            StartTimer();
        }

        void StartTimer()
        {            
            Task.Delay(_millisecondsDueTime, Token).ContinueWith(async (t, s) =>
            {
                var tuple = (Tuple<Action<object>, object>)s;

                while (!IsCancellationRequested)
                {
                    if (_waitForCallbackBeforeNextPeriod)
                        tuple.Item1(tuple.Item2);
                    else
                        await Task.Run(() =>
                        {
                            tuple.Item1(tuple.Item2);
                        });

                    await Task.Delay(_millisecondsPeriod, Token).ConfigureAwait(false);
                }

            }, Tuple.Create(_callback, _state), CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Default);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Cancel();

            base.Dispose(disposing);
        }

        internal bool Dispose(WaitHandle handle)
        {
            this.Dispose();

            return false;
        }

        //internal void Change(TimeSpan nextInterval, TimeSpan infiniteTimeSpan)
        //{
        //    _millisecondsDueTime = nextInterval.Milliseconds;
        //    _millisecondsPeriod = infiniteTimeSpan.Milliseconds;

        //    Cancel();

        //    StartTimer();
        //}
    }
}
#endif
