#include <QString>
#include <QtTest>
#include <QCoreApplication>

class ModelsTest : public QObject
{
    Q_OBJECT

public:
    ModelsTest();

private Q_SLOTS:
    void ArenaTableModelTestCase_data();
    void ArenaTableModelTestCase();
};

ModelsTest::ModelsTest()
{
}

void ModelsTest::ArenaTableModelTestCase_data()
{
    QTest::addColumn<QString>("data");
    QTest::newRow("0") << QString();
}

void ModelsTest::ArenaTableModelTestCase()
{
    QFETCH(QString, data);
    QVERIFY2(true, "Failure");
}

QTEST_MAIN(ModelsTest)

#include "tst_modelstest.moc"
