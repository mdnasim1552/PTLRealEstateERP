<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="TaskCodeBook.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_94_Task.TaskCodeBook" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .ddmlist .btn-group button {
            width: 630px;
        }

        .ddmlist .multiselect-container {
            width: 100%;
            overflow-y: scroll !important;
            max-height: 300px !important;
        }

        .chzn-container-multi .chzn-choices .search-field .default {
            color: #999;
            height: 30px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

        .btnadd {
            margin-left: 200px;
        }

        .displayhide {
            display: none;
        }

        .displayshow {
            display: block;
        }

        .btncolortrash {
            background-color: darkred;
        }

            .btncolortrash:hover {
                background-color: red;
            }

        /*input[type=text], select {
            width: 100%;
            padding: 12px 20px;
            margin: 8px 0;
            display: inline-block;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
            height: 25px;
        }*/
    </style>



    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function openModal() {
            //    $('#myModal').modal('show');
            $('#SearchModal').modal('toggle');
        }

        function CloseModal() {

            $('#SearchModal').modal('hide');
        }







        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            //$(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });
            $('.chosen-continer').css('width', '600px');


            $('.chzn-select').chosen({ search_contains: true });

        }
    </script>




    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div class="RealProgressbar">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
            <ProgressTemplate>
                <div id="loader">
                    <div class="dot"></div>
                    <div class="dot"></div>
                    <div class="dot"></div>
                    <div class="dot"></div>
                    <div class="dot"></div>
                    <div class="dot"></div>
                    <div class="dot"></div>
                    <div class="dot"></div>
                    <div class="lading"></div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>


    <div class="card card-fluid">
        <div class="card-body" style="min-height: 600px;">
            <div class="row">
                <div class="col-md-2">
                    <div class="form-group">

                        <label class="control-label">Department:</label>
                        <asp:DropDownList ID="ddldeptcode" runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" TabIndex="2">
                        </asp:DropDownList>


                    </div>
                </div>
                <div class="col-md-1">
                    <asp:LinkButton ID="btnOk" runat="server" CssClass="btn btn-primary mt-4" OnClick="btnOk_Click" TabIndex="11">Select</asp:LinkButton>

                </div>
                <div class="col-md-1">
                    <asp:CheckBox runat="server" ID="chkcopy" Text="Copy" OnCheckedChanged="chkcopy_CheckedChanged" AutoPostBack="true" />
                </div>
            </div>
            <asp:Panel runat="server" ID="pnl" Visible="false">
                <div class="row">

                    <div class="col-md-2">

                        <label class="control-label">From </label>
                        <asp:DropDownList ID="ddlFdeptcode" runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" TabIndex="2">
                        </asp:DropDownList>

                    </div>
                    <div class="col-md-2">

                        <label class="control-label">To </label>
                        <asp:DropDownList ID="ddlTdeptcode" runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" TabIndex="2">
                        </asp:DropDownList>


                    </div>
                    <div class="col-md-1">
                        <asp:LinkButton ID="lnkCopy" runat="server" CssClass="btn btn-primary mt-4" OnClick="lnkCopy_Click" TabIndex="11">Copy</asp:LinkButton>

                    </div>
                </div>
            </asp:Panel>

            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:GridView ID="grvacc" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                            OnRowUpdating="grvacc_RowUpdating" ShowFooter="True" Width="600px"
                            PageSize="15">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous"
                                Visible="False" />
                            <%-- <FooterStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />--%>
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                    SelectText="" ShowEditButton="True">
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px"
                                            MaxLength="6"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "taskcode1")) %>'
                                            Width="40px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "taskcode1")) %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "taskdesc")) %>'
                                            Width="250px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlPageNo" runat="server" AutoPostBack="True"
                                            Font-Bold="True" Font-Size="14px"
                                            OnSelectedIndexChanged="ddlPageNo_SelectedIndexChanged"
                                            Style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid"
                                            Width="150px">
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbldesc" runat="server" Font-Size="12px" Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "taskdesc")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "taskcode")) %>'
                                            Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active">
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkActive" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "active"))=="True" %>' />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtype" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "active")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <FooterStyle CssClass="grvFooter" />
                            <AlternatingRowStyle BackColor="" />
                        </asp:GridView>


                    </div>
                </div>


            </div>

            <div class="row text-center">
                <div class="col-md-12">
                    <div class="form-group">

                        <asp:LinkButton ID="lUpdatPerInfo" Visible="false" runat="server" CssClass="btn btn-danger  btn-xs" OnClientClick="return confirm('Do You want to Update?');">Update</asp:LinkButton>
                    </div>
                </div>
            </div>







        </div>
    </div>



    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

