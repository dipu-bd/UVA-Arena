#include <QString>
#include <QtTest>

class UnitTest : public QObject
{
    Q_OBJECT

public:
    UnitTest();

private Q_SLOTS:
    void testCase1();
};

UnitTest::UnitTest()
{
}

void UnitTest::testCase1()
{
    QVERIFY2(true, "Failure");
}

QTEST_APPLESS_MAIN(UnitTest)

#include "tst_unittest.moc"
