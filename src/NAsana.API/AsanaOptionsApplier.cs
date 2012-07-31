namespace NAsana.API.v1
{
    using RestSharp;

    public class AsanaOptionsApplier
    {
        private readonly AsanaOptions _options;

        public AsanaOptionsApplier(AsanaOptions options)
        {
            _options = options;
        }

        public void ApplyOptions(IRestRequest request, bool isGet)
        {
            if (_options == null)
            {
                return;
            }
            ApplyOptionsStrategy applyOptionsStrategy = isGet
                                                            ? (ApplyOptionsStrategy)
                                                              new GetApplyOptionsStrategy(request, _options)
                                                            : new PostApplyOptionsStrategy(request, _options);
            if (_options.Pretty)
            {
                applyOptionsStrategy.ApplyPretty();
            }

            if (_options.Fields != null && _options.Fields.Count > 0)
            {
                applyOptionsStrategy.ApplyFields();
            }

            if (_options.Expand != null && _options.Expand.Count > 0)
            {
                applyOptionsStrategy.ApplyExpand();
            }
            applyOptionsStrategy.Finish();
        }

        #region Nested type: ApplyOptionsStrategy

        private abstract class ApplyOptionsStrategy
        {
            protected readonly AsanaOptions Options;
            protected readonly IRestRequest Request;

            protected ApplyOptionsStrategy(IRestRequest request, AsanaOptions options)
            {
                Request = request;
                Options = options;
            }

            public abstract void ApplyPretty();
            public abstract void ApplyFields();
            public abstract void ApplyExpand();
            public abstract void Finish();
        }

        #endregion

        #region Nested type: GetApplyOptionsStrategy

        private class GetApplyOptionsStrategy : ApplyOptionsStrategy
        {
            public GetApplyOptionsStrategy(IRestRequest request, AsanaOptions options) : base(request, options)
            {
            }

            public override void ApplyPretty()
            {
                Request.AddParameter("opt_pretty", "true");
            }

            public override void ApplyFields()
            {
                Request.AddParameter("opt_fields", string.Join(",", Options.Fields));
            }

            public override void ApplyExpand()
            {
                Request.AddParameter("opt_expand", string.Join(",", Options.Expand));
            }

            public override void Finish()
            {
            }
        }

        #endregion

        #region Nested type: PostApplyOptionsStrategy

        private class PostApplyOptionsStrategy : ApplyOptionsStrategy
        {
            private readonly options _postOpt = new options();

            public PostApplyOptionsStrategy(IRestRequest request, AsanaOptions options) : base(request, options)
            {
            }

            public override void ApplyPretty()
            {
                _postOpt.pretty = true;
            }

            public override void ApplyFields()
            {
                _postOpt.fields = Options.Fields.ToArray();
            }

            public override void ApplyExpand()
            {
                _postOpt.expand = Options.Expand.ToArray();
            }

            public override void Finish()
            {
                Request.AddParameter("options", _postOpt);
            }
        }

        #endregion

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Local

        #region Nested type: options

        private class options
        {
            public bool pretty { get; set; }
            public string[] fields { get; set; }
            public string[] expand { get; set; }
        }

        #endregion

// ReSharper restore InconsistentNaming
// ReSharper restore UnusedAutoPropertyAccessor.Local
    }
}