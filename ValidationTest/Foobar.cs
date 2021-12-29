namespace ValidationTest
{
	public class Foobar
	{
		public string Bar { get; set; }

		[NotAllowedValueIf("foo", nameof(Bar), "Bar")]
		[NotAllowedValueIf("foo1", nameof(Bar), "Bar2")]
		public string Foo { get; set; }
	}
}
