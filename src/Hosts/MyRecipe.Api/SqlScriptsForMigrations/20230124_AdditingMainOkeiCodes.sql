
CREATE TEMPORARY TABLE "NewOkeis" (
	"Code" text NOT NULL,
    "Name" text NOT NULL,
    "ConventionDesignationNational" text,
    "ConventionDesignationInternational" text,
    "CodeDesignationNational" text,
    "CodeDesignationInternational" text
);

INSERT INTO "NewOkeis"(
	"Code",
	"Name",
	"ConventionDesignationNational",
	"ConventionDesignationInternational",
	"CodeDesignationNational",
	"CodeDesignationInternational"
) VALUES
('161', 'Миллиграмм', 'мг', 'mg', 'МГ', 'MGM'),
('163', 'Грамм', 'г', 'g', 'Г', 'GRM'),
('166', 'Килограмм', 'кг', 'kg', 'КГ', 'KGM'),
('111', 'Миллилитр', 'мл', 'ml', 'МЛ', 'MLT'),
('112', 'Литр', 'л', 'l', 'Л', 'LTR'),
('796', 'Штука', 'шт', 'pc', 'ШТ', 'PCE');

MERGE INTO public."Okeis" AS O
USING "NewOkeis" AS N
ON N."Code" = O."Code"
WHEN MATCHED THEN
	UPDATE SET
		"Name" = N."Name",
		"ConventionDesignationNational" = N."ConventionDesignationNational",
		"ConventionDesignationInternational" = N."ConventionDesignationInternational",
		"CodeDesignationNational" = N."CodeDesignationNational",
		"CodeDesignationInternational" = N."CodeDesignationInternational"
WHEN NOT MATCHED THEN
	INSERT (
		"Code",
		"Name",
		"ConventionDesignationNational",
		"ConventionDesignationInternational",
		"CodeDesignationNational",
		"CodeDesignationInternational"
	) VALUES (
		N."Code",
		N."Name",
		N."ConventionDesignationNational",
		N."ConventionDesignationInternational",
		N."CodeDesignationNational",
		N."CodeDesignationInternational"
	);

DROP TABLE "NewOkeis";

SELECT * FROM public."Okeis";
