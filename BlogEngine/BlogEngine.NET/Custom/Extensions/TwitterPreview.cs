using System;
using System.Web;
using System.Web.UI;
using BlogEngine.Core;
using BlogEngine.Core.Web.Controls;
using BlogEngine.Core.Web.Extensions;
using Page = System.Web.UI.Page;

namespace BlogEngine.NET.Custom.Extensions
{
	[Extension(
		"Inserts metatags to make cool twitter preview card from post",
		"1.0",
		"Sergei Sergeev",
		1020)]
	public class TwitterPreview
	{
		static TwitterPreview()
		{
			Post.Serving += PostServing;
		}

		private static void PostServing(object sender, ServingEventArgs e)
		{
			try
			{
				if (!ExtensionManager.ExtensionEnabled("TwitterPreview"))
					return;

				switch (e.Location)
				{
					case ServingLocation.SinglePost:
						InsertTwitterMetatags(e, sender as Post);
						break;
				}

			}
			catch (Exception)
			{

			}
		}

		private static void InsertTwitterMetatags(ServingEventArgs e, Post post)
		{
			var context = HttpContext.Current;

			if (!(context?.CurrentHandler is Page)) return;

			var page = (Page)context.CurrentHandler;

			AddMetaTag("twitter:site", "@sergeev_srg", page);
			AddMetaTag("twitter:creator", "@sergeev_srg", page);
			AddMetaTag("twitter:title", post.Title, page);
			AddMetaTag("twitter:description", GetDescription(e.Body) + "... [Click to read more]", page);

			var imageSrc = post.FirstImgSrc;
			if (string.IsNullOrEmpty(imageSrc))
			{
				// set generic image src
				imageSrc = "/image.axd?picture=/sp-blog.png";
				AddMetaTag("twitter:card", "summary", page);
			}
			else
			{
				AddMetaTag("twitter:card", "summary_large_image", page);
			}

			AddMetaTag("twitter:image", new Uri(Utils.AbsoluteWebRoot, imageSrc).AbsoluteUri, page);
		}

		private static void AddMetaTag(string name, string value, Page page)
		{
			if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
				return;

			const string tag = "\n\t<meta name=\"{0}\" content=\"{1}\" />";
			page.Header.Controls.Add(new LiteralControl(string.Format(tag, name, value)));
		}

		static string GetDescription(string content)
		{
			var p = Utils.StripHtml(content);

			if (p.Length > 250)
				return p.Substring(0, 250);

			return p;
		}
	}
}