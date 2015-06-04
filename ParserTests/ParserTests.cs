using Infrastructure.Model;
using Infrastructure.Oxm;
using Newtonsoft.Json;
using NUnit.Framework;
using ParserTests.TestMocks;

namespace ParserTests
{
    [TestFixture]
    public class ParserTests : TestHelper
    {
        [SetUp]
        public new void SetUp()
        {
            base.SetUp();
            MapperProvider.Register(new OfferNewModelOxm());
            MapperProvider.Register(new OfferOldModelOxm());
            MapperProvider.Register(new OfferTestOxm());
            MapperProvider.Register(new ParamOxm());
            MapperProvider.Register(new PictureOxm());
            MapperProvider.Register(new PictureOrigOxm());
        }

        [Test]
        public void XmlSameParameterTest()
        {
            const string xmlString = @"
<offer_test>
    <param name='Green'>con1</param>
    <param name='Collection'>con2</param>
    <param name='Season'>con3</param>
    <param name='Manufacturer'>con4</param>
    <param name='Size'>con5</param>
    <param name='Gender'>con6</param>
    <param name='Age'>con7</param>
</offer_test>";
            var xml = ReadXElement(xmlString);

            var oxm = MapperProvider.GetMapper<OfferTest>();

            var objectResult = oxm.ReadType(xml);
            var xmlResult = oxm.Write(objectResult);

            AssertEqual(xml, xmlResult);
        }
        
        [Test]
        public void NewOfferTest()
        {
            const string xmlString = @"
<offer available='true' group_id='700847' type='vendor.model' id='HE002EMEYG24INXL'>
	<categoryId>184</categoryId>
	<cpa>1</cpa>
	<currencyId>RUB</currencyId>
	<delivery>true</delivery>
	<description>Рубашка Mango Man. Цвет: голубой.  Сезон: Весна-лето 2015.</description>
	<discount>0</discount>
	<manufacturer_warranty>true</manufacturer_warranty>
	<market_category>Одежда, обувь и аксессуары/Мужская одежда/Одежда/Рубашки</market_category>
	<model>- CHARLES4</model>
	<name>Рубашка Mango Man</name>
	<param name='Цвет'>голубой</param>
	<param name='Коллекция'>Весна-лето 2015</param>
	<param name='Сезонность'>Мульти</param>
	<param name='Страна-изготовитель'>Китай</param>
	<param name='Размер' unit='INT'>52</param>
	<param name='Пол'>Мужской</param>
	<param name='Возраст'>Взрослый</param>
	<pickup>true</pickup>
	<picture>http://pi.lmcdn.ru/img600x866/H/E/HE002EMEYG24_1_v1.jpg</picture>
	<picture>http://pi.lmcdn.ru/img600x866/H/E/HE002EMEYG24_2_v1.jpg</picture>
	<picture>http://pi.lmcdn.ru/img600x866/H/E/HE002EMEYG24_3_v1.jpg</picture>
	<price>2999</price>
	<sales_notes>Оплата после примерки.</sales_notes>
	<store>true</store>
	<typePrefix>Рубашка</typePrefix>
	<vendor>Mango Man</vendor>
	<vendorCode>44047528</vendorCode>
	<url>https://ad.admitad.com/goto/3f2779c2d435ca72f0194e8640d77b/?i=5&amp;ulp=http%3A%2F%2Fwww.lamoda.ru%2Fp%2FHE002EMEYG24%2F</url>
	<modified_time>1432562628</modified_time>
</offer>";
            var xml = ReadXElement(xmlString);

            var oxm = MapperProvider.GetMapper<OfferNewModel>();

            var objectResult = oxm.ReadType(xml);
            var xmlResult = oxm.Write(objectResult);

            AssertEqual(xml, xmlResult);
        }
        
        [Test]
        public void OldOfferTest()
        {
            const string xmlString = @"
<offer available='true' original_id='830631' type='vendor.model' id='39767420'>
	<delivery>true</delivery>
	<description>Рубашка Marciano Guess. Цвет: синий. Сезон: Осень-зима 2014/2015. С бесплатной доставкой и примеркой на Lamoda.</description>
	<manufacturer_warranty>true</manufacturer_warranty>
	<model>Marciano Guess GU643EMBWE15</model>
	<picture_orig>http://pi.lmcdn.ru/img600x866/G/U/GU643EMBWE15_1.jpg</picture_orig>
	<picture_orig>http://pi.lmcdn.ru/img600x866/G/U/GU643EMBWE15_3.jpg</picture_orig>
	<picture_orig>http://pi.lmcdn.ru/img600x866/G/U/GU643EMBWE15_4.jpg</picture_orig>
	<picture_orig>http://pi.lmcdn.ru/img600x866/G/U/GU643EMBWE15_6.jpg</picture_orig>
	<picture_orig>http://pi.lmcdn.ru/img600x866/G/U/GU643EMBWE15_7.jpg</picture_orig>
	<picture_orig>http://pi.lmcdn.ru/img600x866/G/U/GU643EMBWE15_8.jpg</picture_orig>
	<picture_orig>http://pi.lmcdn.ru/img600x866/G/U/GU643EMBWE15_9.jpg</picture_orig>
	<sales_notes>Оплата после примерки. Финальные скидки до 70%</sales_notes>
	<typePrefix>Рубашка</typePrefix>
	<vendorCode>43M405 4028Z</vendorCode>
	<param name='Цвет'>синий</param>
	<param name='Коллекция'>Осень-зима 2014/2015</param>
	<param name='Сезонность'>Мульти</param>
	<param name='Страна-изготовитель'>Турция</param>
	<param name='Unit=INT'>46/48</param>
	<param name='Пол'>Мужской</param>
	<param name='Возраст'>Взрослый</param>
	<categoryId>1883</categoryId>
	<vendor>MARCIANO Guess</vendor>
	<oldprice>7990.0</oldprice>
	<price>7190.0</price>
	<advcampaign_id>1001</advcampaign_id>
	<advcampaign_name>Lamoda RU</advcampaign_name>
	<modified_time>1424234098</modified_time>
	<picture>http://cdn.admitad.com/products/pictures/1001/5/8/581400cb-557004.jpeg</picture>
	<thumbnail>http://cdn.admitad.com/products/thumbnails/1001/9/a/9a2bd2bf-557004.jpeg</thumbnail>
	<url>http://ad.admitad.com/goto/3f2779c2d435ca72f0194e8640d77b/?i=5&amp;ulp=http%3A%2F%2Fwww.lamoda.ru%2Fp%2FGU643EMBWE15%2F</url>
	<currencyId>RUB</currencyId>
	<name>Рубашка Marciano Guess</name>
</offer>";
            var xml = ReadXElement(xmlString);

            var oxm = MapperProvider.GetMapper<OfferOldModel>();

            var objectResult = oxm.ReadType(xml);
            var xmlResult = oxm.Write(objectResult);

            AssertEqual(xml, xmlResult);
        }

        [Test]
        public void JsonConverterTests()
        {
            const string jsonStr = @"
[{'verified': false, 'name': '\u041e\u0431\u0443\u0432\u044c', 'campaign_id': 1864, 'admitad_category_id': 1824, 'parent_id': null, 'original_id': '785', 'campaign_name': 'Klimini Club', 'admitad_id': 4160}, {'verified': false, 'name': '\u0421\u0443\u043c\u043a\u0438 \u0438 \u043a\u043e\u0436\u0433\u0430\u043b\u0430\u043d\u0442\u0435\u0440\u0435\u044f', 'campaign_id': 1864, 'admitad_category_id': null, 'parent_id': 4160, 'original_id': '2089', 'campaign_name': 'Klimini Club', 'admitad_id': 4161}, {'verified': false, 'name': '\u041a\u043e\u0436\u0433\u0430\u043b\u0430\u043d\u0442\u0435\u0440\u0435\u044f', 'campaign_id': 1864, 'admitad_category_id': null, 'parent_id': 4161, 'original_id': '2091', 'campaign_name': 'Klimini Club', 'admitad_id': 4162}, {'verified': false, 'name': '\u0416\u0435\u043d\u0441\u043a\u0430\u044f \u043e\u0431\u0443\u0432\u044c', 'campaign_id': 1864, 'admitad_category_id': null, 'parent_id': 4160, 'original_id': '797', 'campaign_name': 'Klimini Club', 'admitad_id': 4163}, {'verified': false, 'name': '\u041c\u043e\u043a\u0430\u0441\u0438\u043d\u044b', 'campaign_id': 1864, 'admitad_category_id': 1835, 'parent_id': 4163, 'original_id': '2093', 'campaign_name': 'Klimini Club', 'admitad_id': 4164}, {'verified': false, 'name': '\u041c\u0443\u0436\u0441\u043a\u0430\u044f \u043e\u0431\u0443\u0432\u044c', 'campaign_id': 1864, 'admitad_category_id': null, 'parent_id': 4160, 'original_id': '786', 'campaign_name': 'Klimini Club', 'admitad_id': 4165}, {'verified': false, 'name': '\u0411\u043e\u0442\u0438\u043d\u043a\u0438', 'campaign_id': 1864, 'admitad_category_id': 1843, 'parent_id': 4165, 'original_id': '787', 'campaign_name': 'Klimini Club', 'admitad_id': 4166}, {'verified': false, 'name': '\u041a\u0440\u043e\u0441\u0441\u043e\u0432\u043a\u0438', 'campaign_id': 1864, 'admitad_category_id': 1837, 'parent_id': 4165, 'original_id': '789', 'campaign_name': 'Klimini Club', 'admitad_id': 4167}, {'verified': false, 'name': '\u0421\u0430\u043d\u0434\u0430\u043b\u0438\u0438 ', 'campaign_id': 1864, 'admitad_category_id': 389, 'parent_id': 4165, 'original_id': '791', 'campaign_name': 'Klimini Club', 'admitad_id': 4168}, {'verified': false, 'name': '\u041c\u043e\u043a\u0430\u0441\u0438\u043d\u044b', 'campaign_id': 1864, 'admitad_category_id': 1835, 'parent_id': 4165, 'original_id': '793', 'campaign_name': 'Klimini Club', 'admitad_id': 4169}, {'verified': false, 'name': '\u0422\u0443\u0444\u043b\u0438', 'campaign_id': 1864, 'admitad_category_id': 395, 'parent_id': 4165, 'original_id': '795', 'campaign_name': 'Klimini Club', 'admitad_id': 4170}, {'verified': false, 'name': '\u0421\u0430\u043f\u043e\u0433\u0438', 'campaign_id': 1864, 'admitad_category_id': 1830, 'parent_id': 4163, 'original_id': '798', 'campaign_name': 'Klimini Club', 'admitad_id': 4171}, {'verified': false, 'name': '\u0411\u043e\u0442\u0444\u043e\u0440\u0442\u044b', 'campaign_id': 1864, 'admitad_category_id': 1842, 'parent_id': 4163, 'original_id': '800', 'campaign_name': 'Klimini Club', 'admitad_id': 4172}, {'verified': false, 'name': '\u0411\u0430\u043b\u0435\u0442\u043a\u0438', 'campaign_id': 1864, 'admitad_category_id': 1846, 'parent_id': 4163, 'original_id': '802', 'campaign_name': 'Klimini Club', 'admitad_id': 4173}, {'verified': false, 'name': '\u0411\u043e\u0441\u043e\u043d\u043e\u0436\u043a\u0438', 'campaign_id': 1864, 'admitad_category_id': 382, 'parent_id': 4163, 'original_id': '806', 'campaign_name': 'Klimini Club', 'admitad_id': 4174}, {'verified': false, 'name': '\u0411\u043e\u0442\u0438\u043b\u044c\u043e\u043d\u044b', 'campaign_id': 1864, 'admitad_category_id': 1844, 'parent_id': 4163, 'original_id': '809', 'campaign_name': 'Klimini Club', 'admitad_id': 4175}, {'verified': false, 'name': '\u0421\u0443\u043c\u043a\u0438 \u0436\u0435\u043d\u0441\u043a\u0438\u0435', 'campaign_id': 1864, 'admitad_category_id': null, 'parent_id': 4161, 'original_id': '2090', 'campaign_name': 'Klimini Club', 'admitad_id': 4176}, {'verified': false, 'name': 'C\u0430\u0431\u043e', 'campaign_id': 1864, 'admitad_category_id': null, 'parent_id': 4163, 'original_id': '811', 'campaign_name': 'Klimini Club', 'admitad_id': 4177}, {'verified': false, 'name': '\u0421\u0443\u043c\u043a\u0438 \u043c\u0443\u0436\u0441\u043a\u0438\u0435', 'campaign_id': 1864, 'admitad_category_id': null, 'parent_id': 4161, 'original_id': '2092', 'campaign_name': 'Klimini Club', 'admitad_id': 4178}]
";
            var jsonMatchModel = JsonConvert.DeserializeObject<JsonMatchModel[]>(jsonStr);
            
            Assert.IsNotNull(jsonMatchModel);
        }

    }
}
