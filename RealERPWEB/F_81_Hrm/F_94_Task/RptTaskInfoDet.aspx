<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptTaskInfoDet.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_94_Task.RptTaskInfoDet" %>

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
            padding: 10px 20px;
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
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">

                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label">Department:</label>
                                <asp:DropDownList ID="ddldept1" runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddldeptcode_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label">Task:</label>
                                <asp:DropDownList ID="ddltask" runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>

                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label">Employee:</label>
                                <asp:DropDownList ID="ddlEmp" runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>


                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label">From:</label>
                                <asp:TextBox ID="txtfmdt1" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfmdt1"></cc1:CalendarExtender>

                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">

                                <label class="control-label">To:</label>
                                <asp:TextBox ID="txttodt1" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodt1"></cc1:CalendarExtender>


                            </div>
                        </div>

                        <div class="col-md-1">
                            <label class="control-label">Page:</label>
                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control inputTxt"
                                OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                                <asp:ListItem>150</asp:ListItem>
                                <asp:ListItem>200</asp:ListItem>
                                <asp:ListItem>300</asp:ListItem>
                                <asp:ListItem>600</asp:ListItem>
                                <asp:ListItem>900</asp:ListItem>
                            </asp:DropDownList>
                        </div>


                        <div class="col-md-1">
                            <asp:LinkButton ID="lnkSelect" runat="server" CssClass="btn btn-primary mt-4" OnClick="lnkSelect_Click" TabIndex="11">Show</asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:GridView ID="gvShowData" runat="server" AllowPaging="True" OnRowDataBound="gvShowData_RowDataBound"
                                    AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea" ShowFooter="True" Width="600px"
                                    PageSize="15">
                                    <PagerSettings NextPageText="Next" PreviousPageText="Previous"
                                        Visible="False" />
                                    <%-- <FooterStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />--%>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="10px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbfdate" runat="server" Font-Size="10px" Style="font-size: 10px"
                                                    Text='<%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "fdate")).ToString("dd-MMM-yyyy")%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="10px" HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                           <asp:TemplateField HeaderText="Employee">
                                            <HeaderTemplate>
                                                <table style="width: 200px;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                Text="Employee" Width="120px"></asp:Label>
                                                        </td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066"
                                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                            ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                           
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblemployeee" runat="server"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "empdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>

                                            
                                            <ItemStyle HorizontalAlign="left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>     
                                       <%-- <asp:TemplateField HeaderText="Employee">
                                            <ItemTemplate>
                                                <asp:Label ID="lblemployeee" runat="server" Font-Size="10px" Style="font-size: 10px"
                                                    Text='<%#(DataBinder.Eval(Container.DataItem, "empdesc")).ToString()%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="10px" HorizontalAlign="Left" />
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="From Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lblftime" runat="server" Font-Size="10px" Style="font-size: 10px"
                                                    Text='<%#(DataBinder.Eval(Container.DataItem, "ftime")).ToString()%>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="10px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lblttime" runat="server" Font-Size="10px" Style="font-size: 10px"
                                                    Text='<%#(DataBinder.Eval(Container.DataItem, "ttime")).ToString()%>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" Font-Size="10px" Style="font-size: 10px; font-weight: bold;"
                                                    Width="70px">Total:</asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="10px" HorizontalAlign="Left" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Duration">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldur" runat="server" Font-Size="10px" Style="font-size: 10px"
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblttldur" runat="server" Font-Size="10px" Style="font-size: 10px; font-weight: bold;"
                                                    Width="120px"></asp:Label>
                                            </FooterTemplate>

                                            <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="10px" HorizontalAlign="Left" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Activity Description">
                                            <FooterTemplate>
                                            </FooterTemplate>
                                            <ItemTemplate>

                                                <asp:Label ID="lbltaskdesc" runat="server" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "taskdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="10px" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Details">
                                           
                                            <ItemTemplate>

                                                <asp:Label ID="lbltdesc" runat="server" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="10px" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltaskcode" runat="server" Font-Size="10px" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "taskcode")) %>'
                                                    Width="40px"></asp:Label>
                                                <asp:Label ID="lblrowidA" runat="server" Font-Size="10px" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")) %>'
                                                    Width="40px"></asp:Label>
                                                <asp:Label ID="lblfloctncode" runat="server" Font-Size="10px" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "floctn")) %>'
                                                    Width="40px"></asp:Label>
                                                <asp:Label ID="lbltloctncode" runat="server" Font-Size="10px" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tloctn")) %>'
                                                    Width="40px"></asp:Label>
                                                <asp:Label ID="lblempid" runat="server" Font-Size="10px" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empcode")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="From Loc.">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblfloctn" runat="server" Font-Size="10px" Style="font-size: 10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "floctndesc")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="10px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To Location">
                                           
                                            <ItemTemplate>

                                                <asp:Label ID="lbltloctn" runat="server" Font-Size="10px" Style="font-size: 10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tloctndesc")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="10px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrmk" runat="server" Font-Size="10px"
                                                    Text='<%# (DataBinder.Eval(Container.DataItem, "rmk")).ToString() %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="10px" HorizontalAlign="Left" />
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





                </asp:View>

            </asp:MultiView>


        </div>

    </div>

    

    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>



