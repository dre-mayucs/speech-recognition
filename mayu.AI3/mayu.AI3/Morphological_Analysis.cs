using NMeCab;

namespace mayu.AI3
{
    class Morphological_Analysis
    {
        private MeCabParam param = new MeCabParam();
        private string cache;
        private string[] data;
        public string[] Analysis(string Content)
        {
           try
            {
                param.DicDir = "dic/ipadic";
                MeCabTagger tagger = MeCabTagger.Create(param);
                MeCabNode node = tagger.ParseToNode(Content);

                while (node != null)
                {
                    if (node.CharType > 0)
                        cache += node.Surface + ',';

                    node = node.Next;
                }
                tagger.Dispose();

                data = cache.Split(',');
            }
            catch
            {
                var ER = new Error();
                ER.ErrorMsg("E:201");
            }

            return data;
        }
    }
}
