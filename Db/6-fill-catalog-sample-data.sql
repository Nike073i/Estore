INSERT INTO Author (FirstName, MiddleName, LastName, Description, AuthorImage, UniqueId) 
	SELECT 'Михаил', 'Евгеньевич', 'Фленов', 'Флёнов Михаил, профессиональный программист. Работал в журнале «Хакер», в котором несколько лет вел рубрики «Hack-FAQ» и «Кодинг» для программистов, печатался в журналах «Игромания» и «Chip-Россия». Автор бестселлеров «Библия Delphi», «Программирование в Delphi глазами хакера», «Программирование на C++ глазами хакера», «Компьютер глазами хакера» и др. Некоторые книги переведены на иностранные языки и изданы в США, Канаде, Польше и других странах.',
		'https://bhv.ru/wp-content/uploads/2021/03/Flenov-Mihail-150x150.jpg',
		'mikhail-flenov'
	WHERE NOT EXISTS (SELECT FROM Author WHERE FirstName = 'Михаил' AND LastName = 'Фленов');

INSERT INTO Author (FirstName, MiddleName, LastName, Description, AuthorImage, UniqueId) 
	SELECT 'Евгений', 'Дмитриевич', 'Умрихин', 'Умрихин Евгений Дмитриевич, кандидат технических наук, инженер-программист в крупной региональной страховой компании. Имеет многолетний опыт разработки и внедрения распределенных IT-решений с использованием веб-сервисов ASP.NET и мобильных платформ Android и iOS. Обладатель ряда авторских свидетельств об официальной регистрации программ для ЭВМ.',
		'https://bhv.ru/wp-content/uploads/2021/02/Umrihin-Evgenij.jpg',
		'umrihin-evgenij'
	WHERE NOT EXISTS (SELECT FROM Author WHERE FirstName = 'Евгений' AND LastName = 'Умрихин');

INSERT INTO Author (FirstName, MiddleName, LastName, Description, AuthorImage, UniqueId) 
	SELECT 'Денис', 'Николаевич', 'Колисниченко', 'Колисниченко Денис Николаевич, инженер-программист и системный администратор. Имеет богатый опыт эксплуатации и создания локальных сетей от домашних до уровня предприятия, разработки приложений для различных платформ. Автор более 50 книг компьютерной тематики, в том числе “Microsoft Windows 10. Первое знакомство”, “Самоучитель Microsoft Windows 8”, “Программирование для Android 5. Самоучитель”, “PHP и MySQL. Разработка веб-приложений”, “Планшет и смартфон на базе Android для ваших родителей”, “”Linux. От новичка к профессионалу” и др.',
		'https://bhv.ru/wp-content/uploads/2020/08/Denis_Nikolavich_Kolisnichenko-e1646976464275-200x200.jpg',
		'kolisnichenko-denis-nikolaevich'
	WHERE NOT EXISTS (SELECT FROM Author WHERE FirstName = 'Денис' AND LastName = 'Колисниченко');

INSERT INTO ProductSerie (SerieName) 
	SELECT 'Глазами хакера'
	WHERE NOT EXISTS (SELECT FROM ProductSerie WHERE SerieName = 'Глазами хакера');
	
INSERT INTO ProductSerie (SerieName) 
	SELECT 'Внесерийные книги'
	WHERE NOT EXISTS (SELECT FROM ProductSerie WHERE SerieName = 'Внесерийные книги');
	
INSERT INTO ProductSerie (SerieName) 
	SELECT 'В подлиннике'
	WHERE NOT EXISTS (SELECT FROM ProductSerie WHERE SerieName = 'В подлиннике');
	
INSERT INTO ProductSerie (SerieName) 
	SELECT 'Дерзай! Набор электронных компонентов'
	WHERE NOT EXISTS (SELECT FROM ProductSerie WHERE SerieName = 'Дерзай! Набор электронных компонентов');

INSERT INTO ProductSerie (SerieName) 
	SELECT 'С нуля'
	WHERE NOT EXISTS (SELECT FROM ProductSerie WHERE SerieName = 'С нуля');

INSERT INTO ProductSerie (SerieName) 
	SELECT 'Самоучитель'
	WHERE NOT EXISTS (SELECT FROM ProductSerie WHERE SerieName = 'Самоучитель');

INSERT INTO Category (CategoryName, CategoryUniqueId)
	SELECT 'Компьютеры и программы', 'kompyutery-i-programmy'
	WHERE NOT EXISTS (SELECT FROM Category WHERE CategoryName = 'Компьютеры и программы');

INSERT INTO Category (ParentCategoryId, CategoryName, CategoryUniqueId)
	SELECT 
		(SELECT CategoryId FROM Category WHERE CategoryName = 'Компьютеры и программы'), 
		'Программирование, Языки, Библиотеки', 'programmirovanie_yAzyki_biblioteki'
	WHERE NOT EXISTS (SELECT FROM Category WHERE CategoryName = 'Программирование, Языки, Библиотеки');

INSERT INTO Category (ParentCategoryId, CategoryName, CategoryUniqueId)
	SELECT 
		(SELECT CategoryId FROM Category WHERE CategoryName = 'Компьютеры и программы'), 
		'Сети, Администрирование, Безопасность', 'seti_bezopasnost'
	WHERE NOT EXISTS (SELECT FROM Category WHERE CategoryName = 'Сети, Администрирование, Безопасность');

INSERT INTO Category (ParentCategoryId, CategoryName, CategoryUniqueId)
	SELECT 
		(SELECT CategoryId FROM Category WHERE CategoryName = 'Компьютеры и программы'), 
		'Операционные системы', 'os'
	WHERE NOT EXISTS (SELECT FROM Category WHERE CategoryName = 'Операционные системы');

INSERT INTO Category (ParentCategoryId, CategoryName, CategoryUniqueId)
	SELECT 
		(SELECT CategoryId from Category WHERE CategoryName = 'Компьютеры и программы'), 
		'Разработка Веб приложений', 'razrabotka_veb'
	WHERE NOT EXISTS (SELECT FROM Category WHERE CategoryName = 'Разработка Веб приложений');

INSERT INTO Category (ParentCategoryId, CategoryName, CategoryUniqueId)
	SELECT 
		(SELECT CategoryId FROM Category WHERE CategoryName = 'Операционные системы'), 
		'Linux', 'linux'
	WHERE NOT EXISTS (SELECT FROM Category WHERE CategoryName = 'Linux');

INSERT INTO Category (ParentCategoryId, CategoryName, CategoryUniqueId)
	SELECT 
		(SELECT CategoryId FROM Category WHERE CategoryName = 'Операционные системы'), 
		'Windows', 'windows'
	WHERE NOT EXISTS (SELECT FROM Category WHERE CategoryName = 'Windows');

INSERT INTO Category (ParentCategoryId, CategoryName, CategoryUniqueId)
	SELECT 
		(SELECT CategoryId FROM Category WHERE CategoryName = 'Программирование, Языки, Библиотеки'), 
		'C/C++/C#', 'csharp'
	WHERE NOT EXISTS (SELECT FROM Category WHERE CategoryName = 'C/C++/C#');

INSERT INTO Category (ParentCategoryId, CategoryName, CategoryUniqueId)
	SELECT 
		(SELECT CategoryId from Category WHERE CategoryName = 'Программирование, Языки, Библиотеки'), 
		'Java', 'java'
	WHERE NOT EXISTS (SELECT FROM Category WHERE CategoryName = 'Java');

INSERT INTO Category (ParentCategoryId, CategoryName, CategoryUniqueId)
	SELECT 
		(SELECT CategoryId FROM Category WHERE CategoryName = 'Разработка Веб приложений'), 
		'Веб Сервер', 'veb_server'
	WHERE NOT EXISTS (SELECT FROM Category WHERE CategoryName = 'Веб Сервер');


INSERT INTO Category (CategoryName, CategoryUniqueId)
	SELECT 'Наборы для мейкеров', 'nabory-dlya-mejkerov'
	WHERE NOT EXISTS (SELECT FROM Category WHERE CategoryName = 'Наборы для мейкеров');
	
INSERT INTO Category (CategoryName, CategoryUniqueId)
	SELECT 'Технические науки', 'tehnicheskie-nauki'
	WHERE NOT EXISTS (SELECT FROM Category WHERE CategoryName = 'Технические науки');

INSERT INTO Category (CategoryName, CategoryUniqueId)
	SELECT 'Учебная литература', 'uchebnaya-literatura'
	WHERE NOT EXISTS (SELECT FROM Category WHERE CategoryName = 'Учебная литература');

INSERT INTO Category (CategoryName, CategoryUniqueId)
	SELECT 'Для детей', 'dlya-detej'
	WHERE NOT EXISTS (SELECT FROM Category WHERE CategoryName = 'Для детей');

INSERT INTO Category (CategoryName, CategoryUniqueId)
	SELECT 'Досуг', 'dosug'
	WHERE NOT EXISTS (SELECT FROM Category WHERE CategoryName = 'Досуг');

INSERT INTO Product (CategoryId, ProductName, Price,
				  	Description, ProductImage, ReleaseDate, UniqueId, ProductSerieId)
	SELECT (SELECT CategoryId FROM Category WHERE CategoryName = 'C/C++/C#'),
		'C# глазами хакера',
		560,
		'Подробно рассмотрены  все аспекты безопасности от теории до реальных реализаций .NET-приложений на языке C#. Рассказано, как обеспечивать безопасную регистрацию, авторизацию и поддержку сессий пользователей. Перечислены уязвимости, которые могут быть присущи веб-сайтам и Web API, описано, как хакеры могут эксплуатировать уязвимости  и как можно обеспечить безопасность приложений. Даны основы оптимизации кода для обработки максимального количества пользователей с целью экономии ресурсов серверов и денег на хостинг. Рассмотрены сетевые функции: проверка соединения, отслеживание запроса, доступ к микросервисам, работа с сокетами и др. Приведены реальные примеры атак хакеров и способы защиты от них.',
		'https://bhv.ru/wp-content/uploads/2023/04/2974_978-5-9775-1781-2.jpg',
		'2023-05-01',
		'c-glazami-hakera',
		(SELECT ProductSerieId FROM ProductSerie WHERE SerieName = 'Глазами хакера')
	WHERE NOT EXISTS (SELECT FROM Product WHERE UniqueId = 'c-glazami-hakera');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId from Product WHERE UniqueId = 'c-glazami-hakera'), 'Articul', '2974'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'c-glazami-hakera') AND ParamName = 'Articul');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT	(SELECT ProductId from Product WHERE UniqueId = 'c-glazami-hakera'),'ISBN', '978-5-9775-1781-2'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'c-glazami-hakera') AND ParamName = 'ISBN');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId from Product WHERE UniqueId = 'c-glazami-hakera'), 'Pages', '352'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId from Product WHERE UniqueId = 'c-glazami-hakera') AND ParamName = 'Pages');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId from Product WHERE UniqueId = 'c-glazami-hakera'), 'Print', '0'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId from Product WHERE UniqueId = 'c-glazami-hakera') AND ParamName = 'Print');

INSERT INTO Product (CategoryId, ProductName, Price,
				  	Description, ProductImage, ReleaseDate, UniqueId, ProductSerieId)
	SELECT (SELECT CategoryId FROM Category WHERE CategoryName = 'C/C++/C#'),
		'Библия C#. 5-е изд.',
		866,
		'Книга посвящена программированию на языке C#  для платформы Microsoft .NET, начиная с основ языка и разработки программ для работы в режиме командной строки и заканчивая созданием современных веб-приложений. Материал сопровождается большим количеством практических примеров. Подробно описывается логика выполнения каждого участка программы. Уделено внимание вопросам повторного использования кода. В пятом издании примеры переписаны с учетом современной платформы .NET 5, а вместо прикладных приложений упор делается на веб–приложения. На сайте издательства находятся коды программ, дополнительная справочная информация и копия базы данных для выполнения примеров из книги.',
		'https://bhv.ru/wp-content/uploads/2021/09/2853_978-5-9775-6827-2.jpg',
		'2023-01-01',
		'bibliya-c-5-izd',
		(SELECT ProductSerieId FROM ProductSerie WHERE SerieName = 'Внесерийные книги')
	WHERE NOT EXISTS (SELECT FROM Product WHERE UniqueId = 'bibliya-c-5-izd');


INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'bibliya-c-5-izd'), 'Articul', '2853'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'bibliya-c-5-izd') AND ParamName = 'Articul');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'bibliya-c-5-izd'), 'ISBN', '978-5-9775-6827-2'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'bibliya-c-5-izd') AND ParamName = 'ISBN');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'bibliya-c-5-izd'), 'Pages', '464'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'bibliya-c-5-izd') AND ParamName = 'Pages');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'bibliya-c-5-izd'), 'Print', '0'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'bibliya-c-5-izd') AND ParamName = 'Print');

INSERT INTO Product (CategoryId, ProductName, Price,
				  	Description, ProductImage, ReleaseDate, UniqueId, ProductSerieId)
	SELECT (SELECT CategoryId FROM Category WHERE CategoryName = 'C/C++/C#'),
		'Разработка Android-приложений на С# с использованием Xamarin с нуля',
		894,
		'Рассмотрены особенности создания Android-приложений при помощи Visual Studio Community на C#. Книга знакомит читателя со структурой проектов Xamarin.Android, с особенностями сборки и отладки приложений. Рассматриваются основные подходы к разработке Android-приложений, методы построения интерфейса, хранения данных, показана интеграция мобильных приложений с веб-службами, описаны особенности распространения и публикации приложений в магазине Google Play, разобраны основные методы монетизации мобильного контента. Представлены многочисленные примеры кода для решения различных задач, который можно использовать в своих приложениях.',
		'https://bhv.ru/wp-content/uploads/2021/02/2775_978-5-9775-6671-1.png',
		'2022-10-01',
		'razrabotka-android-prilozhenij-na-s-s-ispolzovaniem-xamarin-s-nulya',
		(SELECT ProductSerieId FROM ProductSerie WHERE SerieName = 'Внесерийные книги')
	WHERE NOT EXISTS (SELECT FROM Product WHERE UniqueId = 'razrabotka-android-prilozhenij-na-s-s-ispolzovaniem-xamarin-s-nulya');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'razrabotka-android-prilozhenij-na-s-s-ispolzovaniem-xamarin-s-nulya'), 'Articul', '2775'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'razrabotka-android-prilozhenij-na-s-s-ispolzovaniem-xamarin-s-nulya') AND ParamName = 'Articul');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'razrabotka-android-prilozhenij-na-s-s-ispolzovaniem-xamarin-s-nulya'), 'ISBN', '978-5-9775-6671-1'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'razrabotka-android-prilozhenij-na-s-s-ispolzovaniem-xamarin-s-nulya') AND ParamName = 'ISBN');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'razrabotka-android-prilozhenij-na-s-s-ispolzovaniem-xamarin-s-nulya'), 'Pages', '304'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'razrabotka-android-prilozhenij-na-s-s-ispolzovaniem-xamarin-s-nulya') AND ParamName = 'Pages');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT 	(SELECT ProductId FROM Product WHERE UniqueId = 'razrabotka-android-prilozhenij-na-s-s-ispolzovaniem-xamarin-s-nulya'),'Print', '0'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'razrabotka-android-prilozhenij-na-s-s-ispolzovaniem-xamarin-s-nulya') AND ParamName = 'Print');

INSERT INTO Product (CategoryId, ProductName, Price, 
				  	Description, ProductImage, ReleaseDate, UniqueId, ProductSerieId)
	SELECT (SELECT CategoryId FROM Category WHERE CategoryName = 'Linux'),
		'Linux глазами хакера. 6-е издание',
		894,
		'Рассмотрены вопросы настройки ОС Linux на максимальную производительность и безопасность. Описано базовое администрирование и управление доступом, настройка Firewall, файлообменный сервер, WEB-, FTP- и Proxy-сервера, программы для доставки электронной почты, службы DNS, а также политика мониторинга системы и архивирование данных. Приведены потенциальные уязвимости, даны рекомендации по предотвращению возможных атак и показано, как действовать при атаке или взломе системы, чтобы максимально быстро восстановить ее работоспособность и предотвратить потерю данных. В шестом издании обновлена информация с учетом последней версии Ubuntu и добавлено описание программ для тестирования безопасности конфигурации ОС. На сайте издательства размещены дополнительная документация и программы в исходных кодах.',
		'https://bhv.ru/wp-content/uploads/2021/03/2790_978-5-9775-6699-5-1.png',
		'2023-01-02',
		'linux-glazami-hakera-6-e-izdanie',
		(SELECT ProductSerieId FROM ProductSerie WHERE SerieName = 'Глазами хакера')
	WHERE NOT EXISTS (SELECT FROM Product WHERE UniqueId = 'linux-glazami-hakera-6-e-izdanie');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'linux-glazami-hakera-6-e-izdanie'), 'Articul', '2790'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'linux-glazami-hakera-6-e-izdanie') AND ParamName = 'Articul');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'linux-glazami-hakera-6-e-izdanie'), 'ISBN', '978-5-9775-6699-5'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'linux-glazami-hakera-6-e-izdanie') AND ParamName = 'ISBN');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'linux-glazami-hakera-6-e-izdanie'), 'Pages', '416'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'linux-glazami-hakera-6-e-izdanie') AND ParamName = 'Pages');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'linux-glazami-hakera-6-e-izdanie'), 'Print', '0' 
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'linux-glazami-hakera-6-e-izdanie') AND ParamName = 'Print');

INSERT INTO Product (CategoryId, ProductName, Price, 
				  	Description, ProductImage, ReleaseDate, UniqueId, ProductSerieId)
	SELECT (SELECT CategoryId FROM Category WHERE CategoryName = 'Веб Сервер'),
		'Web-сервер глазами хакера. 3-е изд.',
		688,
		'Рассмотрена система безопасности web-серверов и типичные ошибки, совершаемые web-разработчиками при написании сценариев на языках PHP, ASP и Perl. Приведены примеры взлома реальных web-сайтов, имеющих уязвимости, в том числе и популярных. В теории и на практике рассмотрены распространенные хакерские атаки: DoS, Include, SQL-инъекции, межсайтовый скриптинг, обход аутентификации и др. Описаны основные приемы защиты от атак и рекомендации по написанию безопасного программного кода, настройка и способы обхода каптчи. В третьем издании рассмотрены новые примеры реальных ошибок, приведены описания наиболее актуальных хакерских атак и методов защиты от них.',
		'https://bhv.ru/wp-content/uploads/2021/06/2833_978-5-9775-6795-4.jpg',
		'2022-11-12',
		'web-server-glazami-hakera-3-e-izd',
		(SELECT ProductSerieId FROM ProductSerie WHERE SerieName = 'Глазами хакера')
	WHERE NOT EXISTS (SELECT FROM Product WHERE UniqueId = 'web-server-glazami-hakera-3-e-izd');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'web-server-glazami-hakera-3-e-izd'), 'Articul', '2833'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'web-server-glazami-hakera-3-e-izd') AND ParamName = 'Articul');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'web-server-glazami-hakera-3-e-izd'), 'ISBN', '978-5-9775-6795-4'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'web-server-glazami-hakera-3-e-izd') AND ParamName = 'ISBN');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'web-server-glazami-hakera-3-e-izd'), 'Pages', '257'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'web-server-glazami-hakera-3-e-izd') AND ParamName = 'Pages');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'web-server-glazami-hakera-3-e-izd'), 'Print', '0'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'web-server-glazami-hakera-3-e-izd') AND ParamName = 'Print');

INSERT INTO Product (CategoryId, ProductName, Price, 
				  	Description, ProductImage, ReleaseDate, UniqueId, ProductSerieId)
	SELECT (SELECT CategoryId FROM Category WHERE CategoryName = 'Linux'),
		'Linux. От новичка к профессионалу. 8 изд.',
		1210,
		'Даны ответы на все вопросы, возникающие при работе с Linux: от установки и настройки этой ОС до настройки сервера на базе Linux. Материал книги максимально охватывает все сферы применения Linux от запуска Windows-игр под управлением Linux до настройки собственного веб-сервера. Также рассмотрены: вход в систему, работа с файловой системой, использование графического интерфейса, установка программного обеспечения, настройка сети и Интернета, работа в Интернете, средства безопасности, резервное копирование, защита от вирусов и другие вопросы. Материал ориентирован на последние версии дистрибутивов Fedora, openSUSE, Slackware, Ubuntu.',
		'https://bhv.ru/wp-content/uploads/2021/11/2822_978-5-9775-6773-2.jpg',
		'2019-09-12',
		'linux-ot-novichka-k-professionalu-8-izd',
		(SELECT ProductSerieId FROM ProductSerie WHERE SerieName = 'В подлиннике')
	WHERE NOT EXISTS (SELECT FROM Product WHERE UniqueId = 'linux-ot-novichka-k-professionalu-8-izd');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'linux-ot-novichka-k-professionalu-8-izd'), 'Articul', '2822'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'linux-ot-novichka-k-professionalu-8-izd') AND ParamName = 'Articul');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'linux-ot-novichka-k-professionalu-8-izd'),'ISBN', '978-5-9775-6773-2'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'linux-ot-novichka-k-professionalu-8-izd') AND ParamName = 'ISBN');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'linux-ot-novichka-k-professionalu-8-izd'), 'Pages', '688'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'linux-ot-novichka-k-professionalu-8-izd') AND ParamName = 'Pages');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'linux-ot-novichka-k-professionalu-8-izd'), 'Print', '0'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'linux-ot-novichka-k-professionalu-8-izd') AND ParamName = 'Print');

INSERT INTO Product (CategoryId, ProductName, Price, 
				  	Description, ProductImage, ReleaseDate, UniqueId, ProductSerieId)
	SELECT (SELECT CategoryId FROM Category WHERE CategoryName = 'Windows'),
		'Самоучитель Microsoft Windows 11',
		787,
		'Описаны как базовые функции, так и основные новинки Windows 11: улучшенный интерфейс системы и новое стартовое меню, полностью переработанный браузер Microsoft Edge, вход на основе биометрии Windows Hello, русскоязычный голосовой ввод, функция работы с многими окнами Snap Layouts.  Рассмотрены среда восстановления и резервное копирование системы, сетевой диск OneDrive, магазин Microsoft Store и другие возможности Windows 11. Особое внимание уделено практическому использованию операционной системы – рассказано, как использовать обновленный файловый менеджер Проводник, как подключиться к Интернету и как решить возможные проблемы с сетевым подключением, как выполнить S.M.A.R.T.-диагностику накопителя и перенести систему на SSD.  Дополнительно описана программа Skype. Книга богато иллюстрирована, что поможет освоить новую систему наглядно и быстро.',
		'https://bhv.ru/wp-content/uploads/2021/11/2880_978-5-9775-6872-2.jpg',
		'2020-04-12',
		'samouchitel-microsoft-windows-11',
		(SELECT ProductSerieId FROM ProductSerie WHERE SerieName = 'Самоучитель')
	WHERE NOT EXISTS (SELECT FROM Product WHERE UniqueId = 'samouchitel-microsoft-windows-11');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'samouchitel-microsoft-windows-11'), 'Articul', '2880'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'samouchitel-microsoft-windows-11') AND ParamName = 'Articul');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'samouchitel-microsoft-windows-11'),'ISBN', '978-5-9775-6829-6'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'samouchitel-microsoft-windows-11') AND ParamName = 'ISBN');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'samouchitel-microsoft-windows-11'), 'Pages', '368'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'samouchitel-microsoft-windows-11') AND ParamName = 'Pages');

INSERT INTO ProductDetail (ProductId, ParamName, StringValue )
SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'samouchitel-microsoft-windows-11'), 'Print', '0'
WHERE NOT EXISTS (SELECT FROM ProductDetail WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'samouchitel-microsoft-windows-11') AND ParamName = 'Print');

INSERT INTO ProductAuthor (ProductId, AuthorId)
	SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'c-glazami-hakera'),
		(SELECT AuthorId from Author WHERE FirstName = 'Михаил' AND LastName = 'Фленов')
	WHERE NOT EXISTS (SELECT FROM ProductAuthor WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'c-glazami-hakera'));
	
INSERT INTO ProductAuthor (ProductId, AuthorId)
	SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'bibliya-c-5-izd'),
		(SELECT AuthorId from Author WHERE FirstName = 'Михаил' AND LastName = 'Фленов')
	WHERE NOT EXISTS (SELECT FROM ProductAuthor WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'bibliya-c-5-izd'));

INSERT INTO ProductAuthor (ProductId, AuthorId)
	SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'razrabotka-android-prilozhenij-na-s-s-ispolzovaniem-xamarin-s-nulya'),
		(SELECT AuthorId FROM Author WHERE FirstName = 'Евгений' AND LastName = 'Умрихин')
	WHERE NOT EXISTS (SELECT FROM ProductAuthor WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'razrabotka-android-prilozhenij-na-s-s-ispolzovaniem-xamarin-s-nulya'));

INSERT INTO ProductAuthor (ProductId, AuthorId)
	SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'linux-glazami-hakera-6-e-izdanie'),
		(SELECT AuthorId FROM Author WHERE FirstName = 'Михаил' AND LastName = 'Фленов')
	WHERE NOT EXISTS (SELECT FROM ProductAuthor WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'linux-glazami-hakera-6-e-izdanie'));

INSERT INTO ProductAuthor (ProductId, AuthorId)
	SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'web-server-glazami-hakera-3-e-izd'),
		(SELECT AuthorId FROM Author WHERE FirstName = 'Михаил' AND LastName = 'Фленов')
	WHERE NOT EXISTS (SELECT FROM ProductAuthor WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'web-server-glazami-hakera-3-e-izd'));

INSERT INTO ProductAuthor (ProductId, AuthorId)
	SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'linux-ot-novichka-k-professionalu-8-izd'),
		(SELECT AuthorId FROM Author WHERE FirstName = 'Денис' AND LastName = 'Колисниченко')
	WHERE NOT EXISTS (SELECT FROM ProductAuthor WHERE ProductId = (SELECT ProductId from Product WHERE UniqueId = 'linux-ot-novichka-k-professionalu-8-izd'));

INSERT INTO ProductAuthor (ProductId, AuthorId)
	SELECT (SELECT ProductId FROM Product WHERE UniqueId = 'samouchitel-microsoft-windows-11'),
		(SELECT AuthorId FROM Author WHERE FirstName = 'Денис' AND LastName = 'Колисниченко')
	WHERE NOT EXISTS (SELECT FROM ProductAuthor WHERE ProductId = (SELECT ProductId FROM Product WHERE UniqueId = 'samouchitel-microsoft-windows-11'));

