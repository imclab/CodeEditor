using CodeEditor.Composition;
using CodeEditor.Logging;
using CodeEditor.Reactive;
using CodeEditor.Server.Interface;
using CodeEditor.Text.UI;
using ServiceStack.Text;

namespace CodeEditor.Languages.Common
{
	[Export(typeof(INavigateToItemProvider))]
	internal class SymbolNavigateToItemProvider : INavigateToItemProvider
	{
		[Import]
		public IObservableServiceClientProvider ServiceClientProvider;

		[Import]
		public ILogger Logger;

		public IObservableX<INavigateToItem> Search(string filter)
		{
			Logger.Log("SymbolNavigateToItemProvider.Search({0})".Fmt(filter));
			return ServiceClient.ObserveMany(new SymbolSearch {Filter = filter}).Select(_ => (INavigateToItem)new SymbolItem(_));
		}

		private IObservableServiceClient ServiceClient
		{
			get { return ServiceClientProvider.Client; }
		}

		internal class SymbolItem : INavigateToItem
		{
			private readonly Symbol _symbol;

			public SymbolItem(Symbol symbol)
			{
				_symbol = symbol;
			}

			public string DisplayText
			{
				get { return _symbol.DisplayText; }
			}

			public void NavigateTo()
			{
				throw new System.NotImplementedException();
			}
		}
	}
}