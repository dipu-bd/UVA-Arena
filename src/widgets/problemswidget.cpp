#include <QNetworkRequest>
#include <QNetworkReply>
#include <QStandardPaths>
#include <QDir>
#include <QFile>
#include <QMessageBox>

#include "problemswidget.h"
#include "ui_problemswidget.h"
#include "mainwindow.h"

using namespace uva;

const QString UVAProblemHTMLUrl = "https://uva.onlinejudge.org/external/%1/%2.html"; // 1 = container, 2 = problem number
const QString UVAProblemPDFUrl = "https://uva.onlinejudge.org/external/%1/%2.pdf"; // 1 = container, 2 = problem number

const QString DefaultWebViewPageHTML =
"<html>"
"<head>"
"<style>"
"h1 { color: #FFF; text-align: center; font-family: Lora, Times New Roman, serif; }"
"</style>"
"</head>"
"<body>"
"<h1>Double click a problem from the problem list to view it here.<h1>"
"</body>"
"</html>";

class ProblemModelStyle : public ModelStyle
{
public:
    virtual QVariant Style(const QModelIndex &index, int role) override
    {
        switch (role)
        {
        case Qt::ForegroundRole:
            switch (index.column())
            {
            case 0:
            case 1:
            case 2:
                return QBrush(Qt::red);

            case 3:
                return QBrush(Qt::cyan);

            default:
                return QBrush(Qt::magenta);
            }

        default:
            return ModelStyle::Style(index, role);
        }
    }
};

ProblemsWidget::ProblemsWidget(QWidget *parent) :
    UVAArenaWidget(parent),
    ui(new Ui::ProblemsWidget)
{
    ui->setupUi(this);
    mProblemsTableModel.setModelStyle(std::make_unique<ProblemModelStyle>());
    mProblemsTableModel.setMaxRowsToFetch(mSettings.maxProblemsTableRowsToFetch());
    mProblemsFilterProxyModel.setSortCaseSensitivity(Qt::CaseInsensitive);
    mProblemsFilterProxyModel.setFilterCaseSensitivity(Qt::CaseInsensitive);
    mProblemsFilterProxyModel.setSourceModel(&mProblemsTableModel);
    mProblemsFilterProxyModel.setFilterKeyColumn(1); // Problem title column
    QObject::connect(ui->searchProblemsLineEdit, &QLineEdit::textChanged,
        &mProblemsFilterProxyModel, &QSortFilterProxyModel::setFilterFixedString);

    ui->problemsTableView->setModel(&mProblemsFilterProxyModel);

    ui->webView->setHtml(DefaultWebViewPageHTML);

}

ProblemsWidget::~ProblemsWidget()
{
    delete ui;
}

void ProblemsWidget::initialize()
{
}

void ProblemsWidget::setProblemsMap(std::shared_ptr<Uhunt::ProblemMap> problemsMap)
{
    mProblemsTableModel.setUhuntProblemMap(problemsMap);
}

void ProblemsWidget::onUVAArenaEvent(UVAArenaEvent arenaEvent, QVariant metaData)
{
    switch (arenaEvent) {
    case UVAArenaEvent::UPDATE_STATUS:
        break;

    default:
        break;
    }
}

void ProblemsWidget::setFilterProblemsBy(QString columnName)
{
    if (columnName == "Problem Number")
        mProblemsFilterProxyModel.setFilterKeyColumn(0);
    else if (columnName == "Problem Title")
        mProblemsFilterProxyModel.setFilterKeyColumn(1);
}

void ProblemsWidget::showNewProblem(int problemNumber)
{
typedef UVAArenaSettings::ProblemFormat ProblemFormat;

    if (mSettings.problemFormatPreference() == ProblemFormat::HTML) {
        ui->webView->setUrl(QUrl(UVAProblemHTMLUrl.arg(problemNumber / 100).arg(problemNumber)));
        ui->problemsWidgetToolbox->setCurrentWidget(ui->problemViewPage);
        ui->documentTabWidget->setCurrentWidget(ui->documentHTMLTab);
    } else { // PDF

        loadPDFByProblemNumber(problemNumber);
        ui->problemsWidgetToolbox->setCurrentWidget(ui->problemViewPage);
        ui->documentTabWidget->setCurrentWidget(ui->documentPDFTab);
    }
}

void ProblemsWidget::problemsTableDoubleClicked(QModelIndex index)
{
    index = mProblemsFilterProxyModel.mapToSource(index);
    int selectedProblemNumber = index.sibling(index.row(), 0).data().toInt();
    showNewProblem(selectedProblemNumber);
}

void ProblemsWidget::loadPDFByProblemNumber(int problemNumber)
{
    QDir saveDirectory(
        QStandardPaths::writableLocation(QStandardPaths::AppDataLocation)
    );

    if (!saveDirectory.exists())
        saveDirectory.mkpath(".");

    if (!saveDirectory.cd("problems")) {
        saveDirectory.mkdir("problems");
        saveDirectory.cd("problems");
    }

    QString pdfFileName = 
        saveDirectory.filePath(tr("%1.pdf").arg(problemNumber));

    if (QFile::exists(pdfFileName))
        ui->pdfViewer->loadDocument(pdfFileName);
    else
        downloadPDF(UVAProblemPDFUrl.arg(problemNumber / 100).arg(problemNumber), pdfFileName);
}

void ProblemsWidget::downloadPDF(const QString &url, const QString &saveFileName)
{
    QNetworkRequest request;
    request.setUrl(QUrl(url));

    QNetworkReply *reply = mNetworkManager->get(request);

    if (reply == nullptr)
        return;

    QObject::connect(reply, &QNetworkReply::finished,
        [this, reply, saveFileName]() {

            if (reply && reply->error() == QNetworkReply::NoError) {

                QByteArray pdfData = reply->readAll();
                ui->pdfViewer->loadDocument(pdfData);

                if (mSettings.savePDFDocumentsOnDownload() && !saveFileName.isEmpty())
                    savePDF(saveFileName, pdfData);
            }

            reply->deleteLater();
        });
}

void ProblemsWidget::savePDF(const QString &fileName, const QByteArray& pdfData)
{
    QFile file(fileName);

    if (!file.open(QIODevice::WriteOnly)) {

        // couldn't open the file
        QMessageBox::critical(this, "Write failure",
            "Could not save pdf file:\n"
            + fileName);

        return;
    }

    QDataStream dataStream(&file);
    dataStream << pdfData;
}