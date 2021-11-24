<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptSalesRegressionFunnel.aspx.cs" Inherits="RealERPWEB.F_21_MKT.RptSalesRegressionFunnel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <style>
        .grvHeader {
            font-family: 'Century Gothic' !important;
            font-size: 16px !important;
        }

        table tr td {
            font-family: 'Century Gothic' !important;
        }

        .chzn-container-single {
            width: 210px !important;
            height: 34px !important;
        }



            .chzn-container-single .chzn-single {
                height: 36px !important;
                line-height: 36px;
            }

        /*  .project-slect  .chzn-container-single{
         width: 100px !important;
            height: 34px !important;
        
        }*/
        .profession-slect .chzn-container-single {
            width: 100px !important;
            height: 34px !important;
        }

        .prcntbox {
            display: block !important;
            color: #ff6a00 !important;
            font-size: 14px !important;
        }
    </style>

    <script type="text/javascript">




        $(document).ready(function () {


            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });


        $('.chzn-container').css('width', '250px');


        function pageLoaded() {



            //$('.datepicker').datepicker({
            //    format: 'mm/dd/yyyy',
            //});



            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });


            $('.chzn-select').chosen({ search_contains: true });



            var gvSummary = $('#<%=this.gvSaleFunnel.ClientID %>');
            gvSummary.Scrollable();


        };
        function openModaldis() {

            $('#mdiscussion').modal('toggle');
            //  $('#lbtntfollowup').click();
        };

    </script>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
    <div class="card card-fluid container-data mt-5">
        <div class="card-body">
          
            <div class="row mb-2" id="divFilter">
                <div class="col-md-3">
                    <div class="input-group input-group-alt">
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary" type="button">From</button>
                        </div>
                        <asp:TextBox ID="txtfodate" ClientIDMode="Static" runat="server" CssClass="form-control  pl-0 pr-0"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                            Format="dd-MMM-yyyy" TargetControlID="txtfodate"></cc1:CalendarExtender>
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary" type="button">To</button>
                        </div>
                        <asp:TextBox ID="txttodate" ClientIDMode="Static" runat="server" CssClass="form-control  pl-0 pr-0"></asp:TextBox>
                        <cc1:CalendarExtender ID="Cal3" runat="server"
                            Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>


                    </div>
                </div>



                <div class="col-md-3 p-0">
                    <div class="input-group input-group-alt">
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary" type="button">Team Lead</button>
                        </div>
                        <asp:DropDownList ID="ddlEmpid" ClientIDMode="Static" data-placeholder="Choose Employee.." runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpid_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>
                </div>

                <div class="col-md-3 p-0">
                    <div class="input-group input-group-alt">
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary" type="button">Projects</button>
                        </div>
                        <asp:DropDownList ID="ddlProject" ClientIDMode="Static" data-placeholder="Choose Projects.." runat="server" CssClass="custom-select chzn-select " AutoPostBack="true" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>
                </div>


                <div class="col-md-3 p-0">
                    <div class="input-group input-group-alt ">
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary" type="button">Profession</button>
                        </div>
                        <asp:DropDownList ID="ddlProfession" data-placeholder="Choose Profession.." runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProfession_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>
                </div>







            </div>
            <div class="row mb-3">
                <div class="col-md-3">
                    <div class="input-group input-group-alt">
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary" type="button">Funnel Stage</button>
                        </div>
                        <asp:DropDownList ID="ddlleadstatus" data-placeholder="Choose Lead Status.." runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlleadstatus_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>
                </div>
                <div class="col-md-3 p-0">
                    <div class="input-group input-group-alt">
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary" type="button">Regression Stage</button>
                        </div>
                        <asp:DropDownList ID="ddlRegression" data-placeholder="Choose Regression.." runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlRegression_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>
                </div>

                <div class="col-md-3 p-0">
                    <div class="input-group input-group-alt">
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary" type="button">Reason</button>
                        </div>
                        <asp:DropDownList ID="ddlReason" data-placeholder="Choose Reason.." runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlReason_SelectedIndexChanged">
                        </asp:DropDownList>
                        <div class="input-group-prepend">
                            <asp:LinkButton ID="LinkButton2" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary okBtn">Ok</asp:LinkButton>

                        </div>
                    </div>
                </div>



                <div class="col-md-2 p-0">

                    <div class="input-group input-group-alt">
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary" type="button">Page</button>
                        </div>

                        <asp:DropDownList ID="ddlpage" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlpage_SelectedIndexChanged">

                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                            <asp:ListItem>150</asp:ListItem>
                            <asp:ListItem>200</asp:ListItem>
                            <asp:ListItem>300</asp:ListItem>
                            <asp:ListItem Selected="True">400</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>
            </div>

            <!-- Tab panes -->
            <div class="row">

                <div class="col-md-12">
                    <asp:GridView ID="gvSaleFunnel" runat="server" AutoGenerateColumns="False"
                        OnPageIndexChanging="gvSaleFunnel_PageIndexChanging"
                        PageSize="15" AllowPaging="true"
                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        OnRowDataBound="gvSaleFunnel_RowDataBound">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: center"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                        ForeColor="Black"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvItmCode" CssClass="desclbll" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "projname")) %>'
                                        Width="150px" ForeColor="Black"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Client Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvempid" runat="server" Visible="false"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamcode")) %>'></asp:Label>

                                    <asp:Label ID="lsircode" runat="server" Visible="false"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proscod")) %>'></asp:Label>




                                    <asp:LinkButton ID="lnkEditfollowup" Width="150px" OnClick="lnkEditfollowup_Click" ForeColor="Chocolate" ClientIDMode="Static" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "clientname")) %>'> </asp:LinkButton>




                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Profession Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvProfession" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "profession")) %>'
                                        Width="150px" ForeColor="Black"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Funnel Stage">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvFunnel" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lststge")) %>'
                                        Width="160px" ForeColor="Black"></asp:Label>
                                   

                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                          
                  
 
 
                            <asp:TemplateField HeaderText="Regression Funnel Stage">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRegression" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstregss")) %>'
                                        Width="160px" ForeColor="Black"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Reason">
                                <ItemTemplate>
                                   <asp:Label ID="lblgvReason" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstreson")) %>'
                                        Width="300px" ForeColor="Black"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>



                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>
                </div>


            </div>

        </div>
    </div>

    <div id="mdiscussion" class="modal fade animated slideInTop " role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-dialog-full-width modal-lg ">
            <div class="modal-content modal-content-full-width">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <i class="fa fa-hand-point-right"></i>
                        Discussion </h4>

                    <button type="button" class="btn btn-xs pull-right" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>


                </div>
                <div class="modal-body ">



                    <div class="row">

                        <div class="col-xs-9 col-sm-9 col-md-9">

                            <p>
                                <strong><span id="lblprosname" runat="server"></span></strong>
                                <br>
                                <strong>Primary : </strong><span id="lblprosphone" runat="server"></span>
                                <br>
                                <strong>Home Address: </strong><span id="lblprosaddress" runat="server"></span>
                                <br>

                                <strong>Notes: </strong><span id="lblnotes" runat="server"></span>
                                <br>
                            </p>

                            <p>

                                <strong>Prefered Area: </strong><span id="lblpreferloc" runat="server"></span>
                                <br>
                                <strong>Appartment Size: </strong><span id="lblaptsize" runat="server"></span>

                                <asp:HiddenField ID="lblproscod" runat="server" />
                                <asp:HiddenField ID="lbleditempid" runat="server" />
                            </p>
                        </div>



                    </div>



                    <div class="row">


                        <div class="col-md-12 col-lg-12">
                            <div class="row">
                                <asp:Repeater ID="rpclientinfo" runat="server">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>


                                        <div class="col-md-12  col-lg-12">
                                            <div class="well">

                                                <div class="col-sm-12 panel">

                                                    <div class=" col-sm-12">

                                                        <p>
                                                            <strong>
                                                                <%# DataBinder.Eval(Container, "DataItem.prosdesc")%> </strong>

                                                            <%# DataBinder.Eval(Container, "DataItem.kpigrpdesc").ToString() %>  on <%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("dd-MMM-yyyy hh:mm tt") %>
                                                            <br>




                                                            <strong>Participants:</strong> <%# DataBinder.Eval(Container, "DataItem.partcilist").ToString() %><br>


                                                            <strong>Summary:</strong><span class="textwrap"><%# DataBinder.Eval(Container, "DataItem.discus").ToString() %></span><br>



                                                            <strong>Next Action:</strong> <%# DataBinder.Eval(Container, "DataItem.nfollowup").ToString() %> on <%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.napnt")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"":Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.napnt")).ToString("dd-MMM-yyyy hh:mm tt")%><br>
                                                            <strong>Comments:</strong> <%# DataBinder.Eval(Container, "DataItem.disgnote").ToString() %>





                                                            <br>
                                                        </p>







                                                    </div>





                                                    <div class="col-md-12 collapse dcomments" id="divreschedule<%# DataBinder.Eval(Container, "DataItem.rownum").ToString() %>">



                                                        <asp:TextBox ID="txtdate" runat="server" ClientIDMode="Static" CssClass=""></asp:TextBox>
                                                        <cc1:CalendarExtender ID="Cal2" runat="server"
                                                            Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>





                                                        Subject:
                                                    <textarea name="lblsubjects" id="lblsubjects" style="width: 300px"></textarea>
                                                        Reason:
                                                    <textarea name="lblreason" id="lblreason" style="width: 300px"></textarea>

                                                        <%--<button type="button" class="btn  btn-success btn-xs" onclick="funReschedule('<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("dd-MMM-yyyy hh:mm tt") %>', '<%# DataBinder.Eval(Container, "DataItem.rownum").ToString() %>')">Post</button>--%>
                                                        <button type="button" class="lbtnschedule">Post</button>

                                                        <input type="hidden" id="lblcdate" value="<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("dd-MMM-yyyy hh:mm tt") %>" />


                                                    </div>



                                                    <%--<asp:LinkButton ID="lbtnComments" CssClass="btn btn-primary btn-xs" runat="server" OnClick="lbtnComments_Click"    data-toggle="collapse" data-target="#dcomments">Comments</asp:LinkButton>--%>



                                                    <div class="col-md-12 collapse dcomments" id="dcomments<%# DataBinder.Eval(Container, "DataItem.rownum").ToString() %>">

                                                        <textarea name="lblcomments" id="lblcomments<%# DataBinder.Eval(Container, "DataItem.rownum").ToString() %>" style="width: 300px"></textarea>
                                                        <br>
                                                        <input type="text" name="txtcomdate" class="datepicker" id="txtcomdate<%# DataBinder.Eval(Container, "DataItem.rownum").ToString() %>" value="<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("MM/dd/yyyy") %>" style="width: 300px"></input>

                                                        <button type="button" class="btn  btn-success btn-xs" id="lbtnpostComments" onclick="funPost('<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("dd-MMM-yyyy hh:mm tt") %>', '<%# DataBinder.Eval(Container, "DataItem.rownum").ToString() %>')">Post</button>



                                                    </div>

                                                    <%--  <button type="button" class="btn btn-primary btn-xs" runat="server" id="Button1" data-toggle="collapse" data-target="#dcomments" >Comments</button>

                                    <div class="col-md-12 collapse "  id="dcomments">

                                      <input type="text"  name="lblcomments" id="lblcomments" />
                                        <button type="button" class="btn  btn-success btn-xs" id="lbtnpostComments"  >Post</button>
                                      


                                    </div>--%>
                                                </div>
                                            </div>
                                        </div>



                                    </ItemTemplate>

                                </asp:Repeater>



                            </div>
                        </div>


                    </div>









                </div>




            </div>
        </div>
    </div>

             </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
